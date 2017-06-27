#!/bin/bash

set -e

dotnet build
dotnet test "Pilcrow.Tests/Pilcrow.Tests.csproj"
