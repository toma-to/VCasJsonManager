# VCasJsonManager リリーススクリプト
# Copyright 2019 TOMA
# MIT License

$ErrorActionPreference = "Stop"

# パス設定
$scriptPath = $MyInvocation.MyCommand.Path
$releaseItemsPath = Split-Path $scriptPath -Parent
$solutionPath = Split-Path $releaseItemsPath -Parent
$objectPath = Join-Path $solutionPath VCasJsonManager\bin\Release
$assemblyCsPath = Join-Path $solutionPath VCasJsonManager\Properties\AssemblyInfo.cs
$slnFilePath = Join-Path $solutionPath VCasJsonManager.sln

# AssemblyInfoからバージョン情報取得
$version = (Get-Content $assemblyCsPath).ForEach{
        if ($_ -match "AssemblyVersion\(`"(\d+\.\d+\.\d+\.\d+)`"\)") {
            $Matches[1]
        }
    }

# 出力先フォルダ構成の作成
$targetPath = Join-Path $releaseItemsPath "bin\VCasJsonManager$version"
New-Item $targetPath -ItemType Directory -Force
Remove-Item "$releaseItemsPath\bin\*" -Recurse

# ビルド実行
$VsEnv = New-Object -ComObject VisualStudio.DTE.16.0
$VsEnv.MainWindow.Visible=$True
$VsEnv.Solution.Open($slnFilePath)
$bld = $VsEnv.Solution.SolutionBuild
$bld.SolutionConfigurations.Item("Release").Activate()
$bld.Clean($True)
$bld.Build($True)
$VsEnv.Quit()

# オブジェクトのコピー
$targetBinaryPath = Join-Path $targetPath "VCasJsonManager"
Copy-Item $objectPath $targetBinaryPath -Recurse
Remove-Item "$targetBinaryPath\*.pdb"

# その他のファイルのコピー
Copy-Item "$solutionPath\LICENSE" $targetPath
Copy-Item "$releaseItemsPath\ReadMe.txt" $targetPath
Copy-Item "$releaseItemsPath\データフォルダ表示.bat" $targetPath
Copy-Item "$releaseItemsPath\Manual\Manual.pdf" "$targetPath\マニュアル.pdf"

# Zip圧縮
Compress-Archive $targetPath "$releaseItemsPath\bin\VCasJsonManager$version.zip"
