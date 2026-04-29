# Infra e Operacao (MVP)

## Git LFS (recomendado)

Arquivos que devem ir para LFS:

- `*.psd`, `*.aseprite`, `*.png` de alta resolução
- `*.wav`, `*.mp3`, `*.ogg`
- `*.fbx`, `*.blend`, `*.obj`
- bundles e binários grandes de build

Evitar LFS para:

- `*.cs`, `*.json`, `*.md`, `*.asset`, `*.meta`

## CI

Workflow incluído em `.github/workflows/unity-windows.yml` com:

- checkout com LFS
- cache de `Library`
- build `StandaloneWindows64` via `game-ci/unity-builder`
- upload de artefato

Segredos necessários no GitHub:

- `UNITY_LICENSE`
- `UNITY_EMAIL`
- `UNITY_PASSWORD`

## Crash reporting

`CrashReportingService` foi adicionado como ponto de integração.
Atualmente registra exceções/erros no log; o próximo passo é enviar para Sentry
ou serviço equivalente por flag/define.

## Telemetria mínima

`TelemetryService` já cobre eventos:

- `RunStart`
- `RunEnd`
- `UpgradePick`
- `Disconnect`

Por enquanto os eventos são logados localmente, prontos para roteamento a backend.
