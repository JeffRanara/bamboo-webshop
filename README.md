# 🎋 Emeraldine Bamboo Webshop (Milestone 1)

Welcome to the standalone repository for the **Emeraldine Webshop Application**. This repository represents a complete, decoupled full-stack transactional milestone designed to manage consumer product browsing, server-side cart tracking, and end-to-end sandbox payment fulfillment for unauthenticated guest users.

---

## 🏗️ Technical Architecture Stack

The platform is engineered using modern, decoupled industry standards to isolate client presentation from relational business and transactional rules:

*   **Frontend Client:** React Single Page Application (SPA) scaffolded with **Vite** for optimized assets compilation and near-instant local hot reloading.
*   **Application Layer:** **ASP.NET Core Web API (net10.0)** serving type-safe REST endpoint structures driven by object-oriented controllers.
*   **Data Persistence Layer:** **PostgreSQL** database managed seamlessly through a Code-First object-relational abstraction layer powered by **Entity Framework Core (EF Core)**.
*   **Merchant Integration:** **Stripe SDK for .NET** processing payment session initialization alongside an asynchronous **Stripe Webhook Consumer** for decoupled order fulfillment.

---

## 🗃️ Database Relational Blueprint

The structural data matrix is normalized to segregate static botanical profiles from retail e-commerce calculations and transactional snapshots:

1.  **`BambooSpecies` & `BambooTechnicalProfile`:** Maintains the core botanical dataset (Genus, Species, hardiness indexes, and technical metadata) via a tight `1:1` relational constraint.
2.  **`PlantVariant` & `ShippingProfile`:** Represents individual commercial configurations (pot sizing metrics, retail pricing tiers) mapped directly to strict spatial logistic metadata used for automatic total calculation.
3.  **`Cart` & `CartItem`:** Ephemeral shopping buffers bound to the server and isolated via unique user identifiers.
4.  **`Order` & `OrderItem`:** Finalized transaction ledgers logging locked unit pricing, total spatial constraints, and unique payment tracking identifiers.

---

## ⚡ Setup & Local Execution Guide

To stand up the complete full-stack environment on your local development machine, follow these steps sequentially:

### 1. Prerequisites
Ensure you have the following software suites installed locally:
*   [.NET 10.0 SDK](https://microsoft.com)
*   [Node.js (v18 or higher)](https://nodejs.org)
*   A running instance of PostgreSQL (Docker Container or Local Native Service)

### 2. Configure Local Environment Secrets
To satisfy GitHub Push Protection requirements, the repository's tracked code configuration contains safe environment placeholders. You must specify your sandbox developer credentials inside the API workspace layer:

1.  Navigate into the API project folder: `Emeraldine.Api/`
2.  Locate or create a file named **`appsettings.Development.json`**.
3.  Inject your credentials on lines 9 and 10 using the following formatting:

```json
{
  "Stripe": {
    "SecretKey": "sk_test_your_actual_stripe_sandbox_secret_key",
    "WebhookSecret": "whsec_your_local_stripe_webhook_signing_secret"
  }
}
```
*Note: This file is fully protected against accidental cloud leaks on your native laptop workspace using configured version control constraints.*
### 3. Initialize and Start the Backend Server
1.  Open the solution file **`Emeraldine.slnx`** inside **Visual Studio**.
2.  Ensure **`Emeraldine.Api`** is highlighted as your default Startup Project.
3.  Press **`Ctrl + F5`** (Start Without Debugging) to spin up the .NET environment. 
4.  Upon execution, the application's **automatic database seeders** will evaluate the database state and cleanly hydrate the PostgreSQL tables with the full product catalog and tiered pricing layers.

### 4. Boot Up the React Frontend Workspace
Open your preferred terminal window and execute these commands to resolve dependencies and launch the client web layout:

```bash
# Change directory into the frontend folder
cd frontend

# Install the matching package dependencies
npm install

# Launch the Vite development server
npm run dev
```
Open your web browser and navigate to **`http://localhost:5173/`** to view and interact with the operational webshop portal.

---

## ⚙️ Core Transactional Flow Architecture

The user transaction lifecycle follows an event-driven paradigm mapping exactly to the system architecture layers:

[React Frontend Client] (UUIDv4 Session Token)
       |
       v (HTTP State Synchronization)
[ASP.NET Core Controllers] (Tally via EF Core)
       |
       v (Secure Handshake Link Generation)
[Stripe Hosted Sandbox Gateway] (Credit Card Process)
       |
       v (Asynchronous Cryptographic Webhook)
[Webhook Event Consumer] ---> [PostgreSQL DB (Order = 'Paid')]
*   Session Maintenance: Guest users are evaluated via a unique client-side UUIDv4 string saved inside browser memory. This string acts as an identity key matching to server-persisted tables so that cart selections survive browser updates.
*   Payment Security: Complete payment operations are offloaded entirely to Stripe's payment layout, meaning zero credit card liability data touches or passes through our local environment.
*   Webhook Idempotency: The background consumer processes event payload callbacks asynchronously, confirming incoming cryptographic signature keys and preventing duplicated order fulfillments.

---

## 🚀 Future Sprints Roadmap

While this release achieves a robust transactional pipeline for unauthenticated users, the next scheduled architectural sprints will focus entirely on platform hardening:
*   Comprehensive User Identity Isolation utilizing ASP.NET Core Identity.
*   Type-safe stateless security access blocks handled via JWT Bearer Token structures.
*   Persistent customer profile indexing tracking historical purchase order records.
