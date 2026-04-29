# Procedimentos de teste do importador LF2 (TRAE).
# Requer: Unity fechado neste projeto (batchmode nao abre se ja houver instancia com o mesmo projectPath).
# Opcional: -ImportAll  importa todos os personagens (type:0) apos o Dennis; demora varios minutos.
param(
    [switch]$ImportAll
)
$ErrorActionPreference = "Stop"
$ProjectRoot = Split-Path $PSScriptRoot -Parent
$Unity = "${env:UNITY_EDITOR_PATH}"
if (-not $Unity -or -not (Test-Path $Unity)) {
    $hub = "C:\Program Files\Unity\Hub\Editor"
    if (Test-Path $hub) {
        $Unity = (Get-ChildItem $hub -Directory | Sort-Object Name -Descending | Select-Object -First 1).FullName + "\Editor\Unity.exe"
    }
}
if (-not (Test-Path $Unity)) {
    Write-Host "Defina UNITY_EDITOR_PATH para Unity.exe ou instale Unity Hub." -ForegroundColor Red
    exit 1
}

# Unity em batch com caminhos com espacos: use "flag=valor" num unico token para o CreateProcess nao partir o path.
# Codigo de saida: Lf2ImporterBatchTest grava Logs/lf2_batch_last_exit.txt (flush) — o ExitCode do processo por vezes e 1 apesar de sucesso.
function Invoke-UnityBatch {
    param(
        [Parameter(Mandatory = $true)][string]$ExecuteMethod,
        [Parameter(Mandatory = $true)][string]$LogFile
    )
    $args = @(
        "-batchmode",
        "-quit",
        "-nographics",
        "-projectPath=$ProjectRoot",
        "-executeMethod", $ExecuteMethod,
        "-logFile=$LogFile"
    )
    $null = Start-Process -FilePath $Unity -ArgumentList $args -Wait -PassThru -NoNewWindow
    $exitFile = Join-Path $ProjectRoot "Logs\lf2_batch_last_exit.txt"
    Start-Sleep -Milliseconds 150
    if (Test-Path $exitFile) {
        $t = (Get-Content $exitFile -Raw -ErrorAction SilentlyContinue).Trim()
        if ($t -match "^\d+$") {
            return [int]$t
        }
    }
    return 1
}

Write-Host "=== Verificacao de caminhos (GameExample em Assets) ===" -ForegroundColor Cyan
$checks = @(
    "Assets\GameExample\LittleFighter\data\data.txt",
    "Assets\GameExample\LittleFighter\data\dennis.dat",
    "Assets\Art\lf2_ref\characters\dennis_0.png"
)
$ok = $true
foreach ($rel in $checks) {
    $p = Join-Path $ProjectRoot $rel
    $e = Test-Path $p
    if (-not $e) { $ok = $false }
    Write-Host ("  {0}: {1}" -f $rel, $(if ($e) { "OK" } else { "FALTA" }))
}
if (-not $ok) {
    Write-Host "Corrija os caminhos antes de importar." -ForegroundColor Red
    exit 2
}

New-Item -ItemType Directory -Force -Path (Join-Path $ProjectRoot "Logs") | Out-Null
$log1 = Join-Path $ProjectRoot "Logs\lf2_decrypt_smoke.log"
# Log do 2. passo sem espacos no path (o Unity por vezes nao cria o ficheiro com espacos no -logFile).
$log2 = Join-Path $env:TEMP "lf2_import_dennis.log"

Write-Host "`n=== Unity batch: RunDecryptSmoke ===" -ForegroundColor Cyan
Write-Host "Unity: $Unity"
$code1 = Invoke-UnityBatch -ExecuteMethod "Lf2ImporterCliEntry.RunDecryptSmoke" -LogFile $log1
if ($null -eq $code1) { $code1 = 1 }
Write-Host "Exit code:" $code1
if ($code1 -ne 0) {
    Write-Host "Falha na compilacao/ILPP. Se vir 'virus ou software indesejado' no log, adicione exclusao do Defender para:" -ForegroundColor Yellow
    Write-Host "  $ProjectRoot\Library" -ForegroundColor Yellow
    Write-Host "Outra causa: outra instancia Unity com este projectPath aberta." -ForegroundColor Yellow
    Get-Content $log1 -Tail 45
    exit $code1
}

Write-Host "`n=== Unity batch: RunDennisImport ===" -ForegroundColor Cyan
$code2 = Invoke-UnityBatch -ExecuteMethod "Lf2ImporterCliEntry.RunDennisImport" -LogFile $log2
if ($null -eq $code2) { $code2 = 1 }
Write-Host "Exit code:" $code2
if (Test-Path $log2) {
    Copy-Item $log2 (Join-Path $ProjectRoot "Logs\lf2_import_dennis.log") -Force -ErrorAction SilentlyContinue
    Get-Content $log2 -ErrorAction SilentlyContinue | Select-String "LF2ImporterBatchTest"
}
else {
    Write-Host "Log do import nao encontrado em TEMP; veja Logs/lf2_batch_last_exit.txt." -ForegroundColor DarkYellow
}

$finalExit = $code2
if ($ImportAll) {
    if ($code2 -ne 0) {
        Write-Host "`n-ImportAll ignorado porque RunDennisImport falhou." -ForegroundColor Yellow
        exit $code2
    }
    $log3 = Join-Path $env:TEMP "lf2_import_all.log"
    Write-Host "`n=== Unity batch: RunImportAll (todos os type:0) ===" -ForegroundColor Cyan
    Write-Host "Relatorio final: Assets/_Imported/LF2/import_report.md"
    $code3 = Invoke-UnityBatch -ExecuteMethod "Lf2ImporterCliEntry.RunImportAll" -LogFile $log3
    if ($null -eq $code3) { $code3 = 1 }
    Write-Host "Exit code:" $code3
    if (Test-Path $log3) {
        Copy-Item $log3 (Join-Path $ProjectRoot "Logs\lf2_import_all.log") -Force -ErrorAction SilentlyContinue
    }
    if ($code3 -ne 0) {
        if (Test-Path $log3) { Get-Content $log3 -Tail 40 }
        exit $code3
    }
    $finalExit = $code3
}

exit $finalExit
