# SuperAuth Makefile

.PHONY: help dev-setup build test benchmark deploy clean

help: ## Show help
	@echo "SuperAuth Development Commands:"
	@echo "  dev-setup    Setup development environment"
	@echo "  build        Build all components"
	@echo "  test         Run all tests"
	@echo "  benchmark    Run performance benchmarks"
	@echo "  deploy       Deploy to staging"
	@echo "  clean        Clean build artifacts"

dev-setup: ## Setup development environment
	@echo "🚀 Setting up SuperAuth development environment..."
	./scripts/setup-dev.sh
	docker-compose up -d
	cargo build --workspace

build: ## Build all Rust components
	@echo "🔨 Building SuperAuth..."
	cargo build --workspace --release

test: ## Run all tests
	@echo "🧪 Running tests..."
	cargo test --workspace
	cd clients/typescript && npm test
	cd clients/python && python -m pytest

benchmark: ## Run performance benchmarks
	@echo "📊 Running benchmarks..."
	./scripts/benchmark.sh
	cargo bench --workspace

deploy: ## Deploy to staging
	@echo "🚀 Deploying to staging..."
	./scripts/deploy.sh staging

clean: ## Clean build artifacts
	@echo "🧹 Cleaning..."
	cargo clean
	docker-compose down
	rm -rf target/
