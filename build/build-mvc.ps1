# COMMON PATHS

$buildFolder = (Get-Item -Path "./" -Verbose).FullName
$slnFolder = Join-Path $buildFolder "../"
$outputFolder = Join-Path $buildFolder "outputs"
$webMvcFolder = Join-Path $slnFolder "src/BookStore.Web.Mvc"

## CLEAR ######################################################################

Remove-Item $outputFolder -Force -Recurse -ErrorAction Ignore
New-Item -Path $outputFolder -ItemType Directory

## RESTORE NUGET PACKAGES #####################################################

Set-Location $slnFolder
dotnet restore

## PUBLISH WEB MVC PROJECT ###################################################

Set-Location $webMvcFolder
dotnet publish --output (Join-Path $outputFolder "Mvc")

## CREATE DOCKER IMAGES #######################################################

# Mvc
Set-Location (Join-Path $outputFolder "Mvc")

docker rmi zero/mvc -f
docker build -t zero/mvc .

## DOCKER COMPOSE FILES #######################################################

Copy-Item (Join-Path $slnFolder "docker/mvc/*.*") $outputFolder

## FINALIZE ###################################################################

Set-Location $outputFolder