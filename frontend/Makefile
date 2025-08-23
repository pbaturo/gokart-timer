# Makefile for Avalonia Frontend (gokart-timer)
# Compatible with Linux, macOS, and Windows (with GNU Make)

# Configuration
PROJECT_NAME := gokart-timer-frontend
PROJECT_FILE := $(PROJECT_NAME).csproj
OUTPUT_DIR := bin
ARTIFACTS_DIR := artifacts
TEST_PROJECT_NAME := $(PROJECT_NAME).Tests
TEST_PROJECT_FILE := $(TEST_PROJECT_NAME)/$(TEST_PROJECT_NAME).csproj

# Detect OS
ifeq ($(OS),Windows_NT)
    PLATFORM := win
    RM := rmdir /s /q
    MKDIR := mkdir
    DOTNET := dotnet.exe
    PATHSEP := \\
else
    PLATFORM := unix
    RM := rm -rf
    MKDIR := mkdir -p
    DOTNET := dotnet
    PATHSEP := /
endif

# Default target
.PHONY: all
all: build

# Help
.PHONY: help
help:
	@echo "Avalonia Frontend Makefile"
	@echo ""
	@echo "Usage:"
	@echo "  make [target]"
	@echo ""
	@echo "Targets:"
	@echo "  build         Build the project in Debug configuration"
	@echo "  release       Build the project in Release configuration"
	@echo "  run           Build and run the project in Debug configuration"
	@echo "  run-release   Build and run the project in Release configuration"
	@echo "  test          Run the project tests"
	@echo "  clean         Clean all build outputs"
	@echo "  restore       Restore NuGet packages"
	@echo "  publish       Create deployable packages for all platforms"
	@echo "  lint          Run code style checks"
	@echo "  help          Show this help message"

# Build (Debug)
.PHONY: build
build:
	$(DOTNET) build $(PROJECT_FILE) --configuration Debug

# Build (Release)
.PHONY: release
release:
	$(DOTNET) build $(PROJECT_FILE) --configuration Release

# Run (Debug)
.PHONY: run
run: build
	$(DOTNET) run --project $(PROJECT_FILE) --configuration Debug

# Run (Release)
.PHONY: run-release
run-release: release
	$(DOTNET) run --project $(PROJECT_FILE) --configuration Release

# Test
.PHONY: test
test:
	@if [ -d "$(TEST_PROJECT_NAME)" ]; then \
		$(DOTNET) test $(TEST_PROJECT_FILE) --configuration Debug; \
	else \
		echo "No test project found."; \
	fi

# Clean
.PHONY: clean
clean:
	$(DOTNET) clean $(PROJECT_FILE)
	$(RM) $(OUTPUT_DIR) 2>/dev/null || true
	$(RM) $(ARTIFACTS_DIR) 2>/dev/null || true
	$(RM) obj 2>/dev/null || true
	@if [ -d "$(TEST_PROJECT_NAME)" ]; then \
		$(RM) $(TEST_PROJECT_NAME)$(PATHSEP)bin 2>/dev/null || true; \
		$(RM) $(TEST_PROJECT_NAME)$(PATHSEP)obj 2>/dev/null || true; \
	fi

# Restore NuGet packages
.PHONY: restore
restore:
	$(DOTNET) restore $(PROJECT_FILE)

# Publish for all platforms
.PHONY: publish
publish: publish-win publish-linux publish-osx

# Publish for Windows
.PHONY: publish-win
publish-win:
	$(MKDIR) $(ARTIFACTS_DIR) 2>/dev/null || true
	$(DOTNET) publish $(PROJECT_FILE) -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o $(ARTIFACTS_DIR)/win-x64

# Publish for Linux
.PHONY: publish-linux
publish-linux:
	$(MKDIR) $(ARTIFACTS_DIR) 2>/dev/null || true
	$(DOTNET) publish $(PROJECT_FILE) -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o $(ARTIFACTS_DIR)/linux-x64

# Publish for macOS
.PHONY: publish-osx
publish-osx:
	$(MKDIR) $(ARTIFACTS_DIR) 2>/dev/null || true
	$(DOTNET) publish $(PROJECT_FILE) -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o $(ARTIFACTS_DIR)/osx-x64

# Create test project if it doesn't exist
.PHONY: create-tests
create-tests:
	@if [ ! -d "$(TEST_PROJECT_NAME)" ]; then \
		$(DOTNET) new xunit -o $(TEST_PROJECT_NAME); \
		$(DOTNET) add $(TEST_PROJECT_NAME)/$(TEST_PROJECT_NAME).csproj reference $(PROJECT_FILE); \
		$(DOTNET) add $(TEST_PROJECT_NAME)/$(TEST_PROJECT_NAME).csproj package Avalonia.Headless; \
		echo "Test project created."; \
	else \
		echo "Test project already exists."; \
	fi

# Lint - check code style
.PHONY: lint
lint:
	$(DOTNET) format $(PROJECT_FILE) --verify-no-changes