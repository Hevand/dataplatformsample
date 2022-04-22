# ISV Data Platform Sample
## Introduction
Independent Software Vendors increasingly offer their solutions as SaaS, hoping to win their customers over to an evergreen, managed version of their application. This direction comes with great benefits for customers and ISVs alike. Unfortunately, there is a challenge that should not be ignored: existing and future end-customer customizations. 

To appreciate the complexity and impact here, let's realize that ISVs have always build, tested, packaged, shipped and largely forgot about their previous software release. It was the customer that was responsible to install and manage the application and database. In that setup, tech-savvy customers could (and did) create solutions on top of the running system. These historic customizations can be a blocker in adopting SaaS, as that same level of flexibility and transparency is not available in SaaS.  

The need for customizations and integrations is not going away with the move to SaaS. Integration capabilities are a key requirement for future apps, reports, AI and other scenarios. 

## Anti-patterns (that you might think of, but shouldn't)
To address this need, ISVs look for simple and fast ways of addressing this challenge:

### 1. Share the database backup files
Backup files are  available for download. Customer restores backup in their own environment. 

| Benefits | Drawbacks |
| --- | --- |
| - Easy setup for ISV | - Tightly coupled with application schema |
| - Low maintenance | - Customer has unrestricted access to all data |
| - No production impact | - Storage-level optimization (polyglot) no longer possible |
| - No developer involvement | - Reporting Scenarios only
|| - No real-time reporting |
|| - Increased backup storage costs |
|| - Complications when using multi-tenant (shared) database |
|| - Customer effort in setting up infrastructure |
|| - Lack of ISV ownership (versioning, clarity, correctness) |

### 2. Replicate the database, give (read-only) access
A secondary instance of the database is created and kept in sync with production. This replica is used for read-only / reporting needs. The customer can setup a direct connection. 

| Benefits | Drawbacks |
| --- | --- |
| - Easy setup for both parties | - Storage-level optimization (polyglot) no longer possible |
| - Low maintenance |  - Reporting Scenarios only |
| - No production impact | - No real-time reporting |
| - Limit to curated views / procedures / versioning possible |- Complications when using multi-tenant (shared) database |

### 3. Reuse existing application APIs
The customer uses the APIs that were designed / planned for internal use. 

| Benefits | Drawbacks |
| --- | --- |
|- Readily available| - Undocumented APIs |
|- Abstraction over application schema| - Reliability and versioning complications, as development team is not involved |
|- Adheres to multi-tenancy model| - Additional, non-distinguishable load on production system |
|- No developer involvement||

## What's in here
In this repository, we're following an alternative approach based on the following architecture: 

0. **Preparation**: Setting up the core infrastructure required to support the full architecture. We'll do this first, such that later steps only need to consider configuration and content.
Topics: 
    - Bicep
0. **API**: Build and deploy an API for basic read/write operations. Setup API management to control access. Topics included:         
    - Product management
    - Discoverability
    - Identity and Access management
    - Resource constraints (throttling, etc)
    - Multitenancy
0. **Scale-out for read operations**: Create a secondary database, using active geo-replication (or equivalent). Update the services to use the scaled-out replica. Topics:
    - Replication
    - connection string modifications
0. **Create Data Warehouse**: Build an ETL process that extracts application data, stores it in a data lake and maps this into a data warehouse. Expose data warehouse capabilities via API management.
    - Data engineering / pipelines
0. **Create ML solutions**: The machine learning team can use the data exposed to train and host a model, exposed via API management.    
    - Cognitive services
    - Training / inference clusters
0. **Customer powerapp**: Build a custom report + 'submit now' button. 
    - Citizen development
    - APIs
    - Security model
    - development lifecycle
0. **Create Data Share invitation process**: On-request: Automate the invitation to Azure Data Share. Topis included:
    - Pattern to get access / automate the process
    - Cross-charging / cost transparency
    - Start / Stop
0. **Create event-based model**: Event-based processing of data in the customer database. Update CosmosDB data store. Expose via api management.

