-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ====================
-- Users table
-- ====================
CREATE TABLE "Users" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(255) NOT NULL,
    "Email" VARCHAR(255) NOT NULL UNIQUE,
    "Password" VARCHAR(255) NOT NULL,
    "AccountNumber" INT,
    "Role" VARCHAR(50) NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ====================
-- Experts table
-- ====================
CREATE TABLE "Experts" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(255) NOT NULL,
    "Description" TEXT NOT NULL,
    "ImgUrl" TEXT,
    "FileContentUrl" TEXT,
    "InitDate" TIMESTAMP WITH TIME ZONE NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ====================
-- Products table
-- ====================
CREATE TABLE "Products" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Name" VARCHAR(255) NOT NULL,
    "Price" DECIMAL(18,2) NOT NULL,
    "MaxVolume" INT NOT NULL,
    "ExpertId" UUID NOT NULL REFERENCES "Experts"("Id") ON DELETE CASCADE,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ====================
-- Sales table
-- ====================
CREATE TABLE "Sales" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "ProductId" UUID NOT NULL REFERENCES "Products"("Id") ON DELETE CASCADE,
    "UserId" UUID NOT NULL REFERENCES "Users"("Id") ON DELETE CASCADE,
    "Status" VARCHAR(50) NOT NULL,
    "Expiration" TIMESTAMP WITH TIME ZONE NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ====================
-- Licenses table
-- ====================
CREATE TABLE "Licenses" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "ProductId" UUID NOT NULL REFERENCES "Products"("Id") ON DELETE CASCADE,
    "UserId" UUID NOT NULL REFERENCES "Users"("Id") ON DELETE CASCADE,
    "Status" VARCHAR(50) NOT NULL,
    "CreatedAt" TIMESTAMP WITH TIME ZONE DEFAULT now()
);


CREATE TABLE "Trades" (
    "Id" UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    "Ticket" INTEGER UNIQUE,
    "Symbol" VARCHAR(50) NOT NULL,
    "Time" INTEGER NOT NULL,
    "Volume" DECIMAL(18,8) NOT NULL,
    "Price" DECIMAL(18,8) NOT NULL,
    "Profit" DECIMAL(18,8) NOT NULL,
    "Type" INTEGER NOT NULL,
    "Magic" BIGINT NOT NULL  
);
