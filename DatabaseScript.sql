Build started...
Build succeeded.
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;
CREATE TABLE "Clients" (
    "ClientId" INTEGER NOT NULL CONSTRAINT "PK_Clients" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "ContactDetails" TEXT NOT NULL,
    "Region" TEXT NOT NULL
);

CREATE TABLE "Contracts" (
    "ContractId" INTEGER NOT NULL CONSTRAINT "PK_Contracts" PRIMARY KEY AUTOINCREMENT,
    "ClientId" INTEGER NOT NULL,
    "StartDate" TEXT NOT NULL,
    "EndDate" TEXT NOT NULL,
    "Status" TEXT NOT NULL,
    "ServiceLevel" TEXT NOT NULL,
    "AgreementFilePath" TEXT NULL,
    CONSTRAINT "FK_Contracts_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("ClientId") ON DELETE CASCADE
);

CREATE TABLE "ServiceRequests" (
    "ServiceRequestId" INTEGER NOT NULL CONSTRAINT "PK_ServiceRequests" PRIMARY KEY AUTOINCREMENT,
    "ContractId" INTEGER NOT NULL,
    "Description" TEXT NOT NULL,
    "CostUSD" TEXT NOT NULL,
    "CostZAR" TEXT NOT NULL,
    "Status" TEXT NOT NULL,
    CONSTRAINT "FK_ServiceRequests_Contracts_ContractId" FOREIGN KEY ("ContractId") REFERENCES "Contracts" ("ContractId") ON DELETE CASCADE
);

CREATE INDEX "IX_Contracts_ClientId" ON "Contracts" ("ClientId");

CREATE INDEX "IX_ServiceRequests_ContractId" ON "ServiceRequests" ("ContractId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260517222328_InitialCreate', '10.0.8');

COMMIT;

BEGIN TRANSACTION;
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260517223331_ClientFix', '10.0.8');

COMMIT;

BEGIN TRANSACTION;
ALTER TABLE "Clients" RENAME COLUMN "ClientId" TO "Id";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260517231051_AddClientId', '10.0.8');

COMMIT;


