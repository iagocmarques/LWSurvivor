# Guia para Agentes: Unity MCP

## Escopo

Este ambiente deve usar apenas o servidor MCP `user-unity-mcp`.

- Nao usar `user-ai-game-developer`.
- Se uma tool necessaria nao estiver habilitada, pedir para o usuario ativar.
- Depois da ativacao, continuar o fluxo normalmente.

## Politica de ativacao durante desenvolvimento

Quando faltar uma capability, seguir este comportamento:

1. Identificar a tool exata que esta faltando.
2. Explicar em 1 frase por que ela e necessaria.
3. Pedir ativacao ao usuario de forma objetiva.
4. Retomar a execucao assim que a tool estiver habilitada.

Template sugerido para pedir ativacao:

`Preciso da tool <NOME_DA_TOOL> para <MOTIVO_CURTO>. Pode ativar ela pra eu continuar?`

## Tools atualmente habilitadas (confirmadas via MCP)

### Assets

- `Unity_AssetGeneration_GenerateAsset`
- `Unity_AssetGeneration_GetModels`

### Core

- `Unity_RunCommand`

### Debug & Diagnostics

- `Unity_GetConsoleLogs`

### Editor

- `Unity_Camera_Capture`
- `Unity_SceneView_Capture2DScene`
- `Unity_SceneView_CaptureMultiAngleSceneView`

## Tools disponiveis no painel (para ativacao sob demanda)

Lista consolidada a partir do painel compartilhado pelo usuario:

### Assets (visiveis no painel)

- `Unity.AssetGeneration.ConvertSpri...`
- `Unity.AssetGeneration.ConvertMaterial`
- `Unity.AssetGeneration.ConvertToTe...`
- `Unity.AssetGeneration.CreateAnima...`
- `Unity.AssetGeneration.EditAnimati...`
- `Unity.AssetGeneration.GetComposit...`
- `Unity.AssetGeneration.ManageInterrupted`
- `Unity.AudioClip_Edit`
- `Unity.FindProjectAssets`
- `Unity.ManageShader`

### Assistant

- `Unity.Grep`

### Core

- `Unity.ApplyTextEdits`
- `Unity.CreateScript`
- `Unity.DeleteScript`
- `Unity.FindInFile`
- `Unity.GetSha`
- `Unity.ImportExternalModel`
- `Unity.ListResources`
- `Unity.ManageAsset`
- `Unity.ManageEditor`
- `Unity.ManageGameObject`
- `Unity.ManageMenuItem`
- `Unity.ManageScene`
- `Unity.ManageScript`
- `Unity.ManageScript.capabilities`
- `Unity.ReadResource`
- `Unity.ScriptApplyEdits`
- `Unity.ValidateScript`

### Debug & Diagnostics

- `Unity.Profiler_GetBottomUpSampleT...`
- `Unity.Profiler_GetFrameGcAllocati...`
- `Unity.Profiler_GetFrameRangeGcAll...`
- `Unity.Profiler_GetFrameRangeTopTimeSummary`
- `Unity.Profiler_GetFrameSelfTimeSa...`
- `Unity.Profiler_GetFrameTopTimeSam...`
- `Unity.Profiler_GetOverallGcAlloca...`
- `Unity.Profiler_GetRelatedSamplesT...`
- `Unity.Profiler_GetSampleGcAllocat...`
- `Unity.Profiler_GetSampleGcAllocat...`
- `Unity.Profiler_GetSampleTimeSumma...`
- `Unity.Profiler_GetSampleTimeSummary`
- `Unity.ReadConsole`

### Editor

- `Unity.GetProjectData`
- `Unity.GetUserGuidelines`
- `Unity.PackageManager_ExecuteAction`
- `Unity.PackageManager_GetData`

## Nota importante

Nomes com `...` acima aparecem truncados no painel. Quando precisar de uma dessas tools, confirmar o nome completo na UI antes de pedir ativacao.
