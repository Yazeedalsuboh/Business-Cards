CREATE TABLE "Business Card"(
    "id" INT NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "Gender" NVARCHAR(255) NOT NULL,
    "DateOfBirth" DATE NOT NULL,
    "Email" NVARCHAR(255) NOT NULL,
    "Phone" NVARCHAR(255) NOT NULL,
    "Photo" NVARCHAR(255) NULL,
    "Address" NVARCHAR(255) NOT NULL
);
ALTER TABLE
    "Business Card" ADD CONSTRAINT "PK_businessCards" PRIMARY KEY("id");