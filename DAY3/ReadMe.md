# Publishing .NET Applications

A comprehensive guide to understanding and implementing Framework-Dependent vs Self-Contained deployments in .NET.

## 📋 Table of Contents
- [Prerequisites](#prerequisites)
- [Key Concepts](#key-concepts)
- [Framework-Dependent Deployment (FDD)](#framework-dependent-deployment-fdd)
- [Self-Contained Deployment (SCD)](#self-contained-deployment-scd)
- [Single-File Publishing](#single-file-publishing)
- [Common Mistakes](#common-mistakes)
- [Recommended Usage](#recommended-usage)
- [Quick Reference](#quick-reference)

## Prerequisites

Before publishing your .NET application, ensure you have:

1. **.NET SDK installed**
   ```bash
   dotnet --version
   ```

2. **Project builds successfully**
   ```bash
   dotnet build
   ```

3. **Navigate to project folder**
   ```bash
   cd ConsoleLearning
   ```
   > ⚠️ **Important**: `dotnet publish` must be run from the project directory (where `.csproj` exists), not the solution directory.

## Key Concepts

### Framework-Dependent Deployment (FDD)
- ✅ Application depends on .NET runtime installed on target machine
- ✅ Smaller output size
- ✅ Faster publish
- ❌ Runtime must be installed separately

### Self-Contained Deployment (SCD)
- ✅ Application includes .NET runtime inside output
- ✅ Runs on machine without .NET installed
- ❌ Larger output size
- ❌ OS-specific

## Framework-Dependent Deployment (FDD)

### When to Use FDD
- Target machine already has .NET installed
- Internal/company servers
- Docker images with .NET base image
- When you want smaller binaries

### Command to Publish (Framework-Dependent)
```bash
dotnet publish -c Release
```
This publishes the application using default framework-dependent mode.

### Output Location
```
bin/
 └── Release/
      └── net8.0/
           └── publish/
```

### Files Generated
- `ConsoleLearning.dll` → Main application
- `ConsoleLearning.deps.json`
- `ConsoleLearning.runtimeconfig.json`

### Run the Application
```bash
dotnet ConsoleLearning.dll
```
❗ Requires matching .NET runtime installed on the machine

### Verify Framework Dependency
Check the `runtimeconfig.json` file:
```json
{
  "framework": {
    "name": "Microsoft.NETCore.App",
    "version": "8.0.0"
  }
}
```

## Self-Contained Deployment (SCD)

### When to Use SCD
- Target machine has no .NET installed
- Client systems
- Desktop utilities
- Exam submission / offline delivery

### Command to Publish (Self-Contained)
```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

### Output Location
```
bin/
 └── Release/
      └── net8.0/
           └── win-x64/
                └── publish/
```

### Files Generated
- `ConsoleLearning.exe` (Windows executable)
- .NET runtime files and dependencies
- No dependency on external runtime

### Run the Application
```bash
ConsoleLearning.exe
```
✅ Works even if .NET is NOT installed

### Verify Self-Contained Mode
Check the runtime config file:
```json
{
  "includedFrameworks": [
    {
      "name": "Microsoft.NETCore.App",
      "version": "8.0.0"
    }
  ]
}
```

## Single-File Publishing

### Framework-Dependent Single File
```bash
dotnet publish -c Release -p:PublishSingleFile=true
```

### Self-Contained Single File
```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```
Output: Single `.exe` file - Best for distribution

## Common Mistakes & Fixes

| Mistake | Fix |
|---------|-----|
| Running from solution folder | `cd ConsoleLearning` |
| Expecting `.exe` in FDD | Use self-contained mode |
| Wrong Runtime Identifier | Specify correct RID (win-x64, linux-x64, osx-x64) |
| Missing dependencies | Check `.deps.json` file |

## Recommended Usage (Industry Practice)

| Scenario | Recommended Mode |
|----------|-----------------|
| Internal servers | Framework-Dependent |
| Client machines | Self-Contained |
| Exam submission | Self-Contained |
| Docker containers | Framework-Dependent |
| Desktop utilities | Self-Contained Single File |
| Web applications | Framework-Dependent |
| Cross-platform tools | Self-Contained |

## Quick Reference

### Common Runtime Identifiers (RID)
- `win-x64` - Windows 64-bit
- `win-x86` - Windows 32-bit
- `linux-x64` - Linux 64-bit
- `osx-x64` - macOS 64-bit
- `osx-arm64` - macOS Apple Silicon

### Publishing Commands Cheat Sheet

```bash
# Framework-Dependent
dotnet publish -c Release

# Self-Contained (Windows)
dotnet publish -c Release -r win-x64 --self-contained true

# Self-Contained Single File
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Trim unused assemblies
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishTrimmed=true
```

## Final Checklist

- [ ] Build succeeds (`dotnet build`)
- [ ] In correct project folder (has `.csproj`)
- [ ] Correct Runtime Identifier (if self-contained)
- [ ] Correct deployment mode selected
- [ ] Verified output in publish directory
- [ ] Tested on target machine/environment

---

## 📝 License

This guide is open-source and available for educational purposes.

## 🤝 Contributing

Found an issue or want to improve this guide? Feel free to submit a PR or open an issue!
