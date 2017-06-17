#!/bin/bash

set -e

dotnet restore
dotnet build
dotnet test "Pilcrow.Tests/Pilcrow.Tests.csproj"

