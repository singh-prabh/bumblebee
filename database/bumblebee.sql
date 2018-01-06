CREATE TABLE "person" (
"id" serial4 NOT NULL,
"firstName" varchar(255),
"lastName" varchar(255),
"companyID" int4,
"email" varchar(255),
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "company" (
"id" serial4 NOT NULL,
"name" varchar(255),
"website" varchar(255),
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "typeLookup" (
"id" int4 NOT NULL,
"description" varchar(255),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "address" (
"id" serial4 NOT NULL,
"personID" int4,
"companyID" int4,
"street1" varchar(255),
"street2" varchar(255),
"city" varchar(255),
"zip" varchar(255),
"stateID" int4,
"typeID" int4,
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "email" (
"id" serial4 NOT NULL,
"address" varchar(255),
"typeID" int4,
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "phone" (
"id" serial4 NOT NULL,
"personID" int4,
"companyID" int4,
"number" int4,
"typeID" int4,
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "task" (
"id" serial4 NOT NULL,
"projectID" int4,
"startDate" timestamp(0),
"endDate" timestamp(0),
"platformID" int4,
"title" varchar(255),
"notes" varchar(500),
"typeID" int4,
"statusID" int4,
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "platform" (
"id" int4 NOT NULL,
"name" varchar(255),
"websiteURL" varchar(255),
"typeID" int4,
"platformCategoryID" int4,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "statusLookup" (
"id" int4 NOT NULL,
"description" varchar(255),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "project" (
"id" serial4 NOT NULL,
"name" varchar(255),
"notes" varchar(500),
"startDate" timestamp(0),
"endDate" timestamp(0),
"statusID" int4,
"companyID" int4,
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "platformCategory" (
"id" int4 NOT NULL,
"description" varchar(255),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "invoice" (
"id" serial4 NOT NULL,
"number" varchar(255),
"dueDate" timestamp(0),
"sentDate" timestamp(0),
"paidDate" timestamp(0),
"depositDate" timestamp(0),
"statusID" int4,
"companyID" int4,
"contactID" int4,
"createdAt" timestamp(0),
"updatedAt" timestamp(0),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "invoiceTasks" (
"id" serial4 NOT NULL,
"invoiceID" int4,
"taskID" int4,
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "states" (
"id" int4 NOT NULL,
"name" varchar(255),
"abbreviation" varchar(255),
PRIMARY KEY ("id") 
)
WITHOUT OIDS;

CREATE TABLE "customer" (
)
WITHOUT OIDS;

CREATE TABLE "table_1" (
)
WITHOUT OIDS;


ALTER TABLE "address" ADD CONSTRAINT "fk_address_type" FOREIGN KEY ("typeID") REFERENCES "typeLookup" ("id");
ALTER TABLE "email" ADD CONSTRAINT "fk_email_type" FOREIGN KEY ("typeID") REFERENCES "typeLookup" ("id");
ALTER TABLE "phone" ADD CONSTRAINT "fk_phone_type" FOREIGN KEY ("typeID") REFERENCES "typeLookup" ("id");
ALTER TABLE "person" ADD CONSTRAINT "fk_person_company" FOREIGN KEY ("companyID") REFERENCES "company" ("id");
ALTER TABLE "platform" ADD CONSTRAINT "fk_platform_type" FOREIGN KEY ("typeID") REFERENCES "typeLookup" ("id");
ALTER TABLE "task" ADD CONSTRAINT "fk_task_project" FOREIGN KEY ("projectID") REFERENCES "project" ("id");
ALTER TABLE "task" ADD CONSTRAINT "fk_task_platform" FOREIGN KEY ("platformID") REFERENCES "platform" ("id");
ALTER TABLE "task" ADD CONSTRAINT "fk_task_type" FOREIGN KEY ("typeID") REFERENCES "typeLookup" ("id");
ALTER TABLE "task" ADD CONSTRAINT "fk_task_status" FOREIGN KEY ("statusID") REFERENCES "statusLookup" ("id");
ALTER TABLE "platform" ADD CONSTRAINT "fk_platform_category" FOREIGN KEY ("platformCategoryID") REFERENCES "platformCategory" ("id");
ALTER TABLE "project" ADD CONSTRAINT "fk_project_status" FOREIGN KEY ("statusID") REFERENCES "statusLookup" ("id");
ALTER TABLE "project" ADD CONSTRAINT "fk_project_company" FOREIGN KEY ("companyID") REFERENCES "company" ("id");
ALTER TABLE "invoice" ADD CONSTRAINT "fk_invoice_status" FOREIGN KEY ("statusID") REFERENCES "statusLookup" ("id");
ALTER TABLE "invoice" ADD CONSTRAINT "fk_invoice_company" FOREIGN KEY ("companyID") REFERENCES "company" ("id");
ALTER TABLE "invoice" ADD CONSTRAINT "fk_invoice_contact" FOREIGN KEY ("contactID") REFERENCES "person" ("id");
ALTER TABLE "invoiceTasks" ADD CONSTRAINT "fk_invoice_tasks_invoice" FOREIGN KEY ("invoiceID") REFERENCES "invoice" ("id");
ALTER TABLE "invoiceTasks" ADD CONSTRAINT "fk_invoice_tasks_task" FOREIGN KEY ("taskID") REFERENCES "task" ("id");
ALTER TABLE "address" ADD CONSTRAINT "fk_address_state" FOREIGN KEY ("stateID") REFERENCES "states" ("id");
ALTER TABLE "address" ADD CONSTRAINT "fk_address_person" FOREIGN KEY ("personID") REFERENCES "person" ("id");
ALTER TABLE "address" ADD CONSTRAINT "fk_address_company" FOREIGN KEY ("companyID") REFERENCES "company" ("id");
ALTER TABLE "phone" ADD CONSTRAINT "fk_phone_person" FOREIGN KEY ("personID") REFERENCES "person" ("id");
ALTER TABLE "phone" ADD CONSTRAINT "fk_phone_company" FOREIGN KEY ("companyID") REFERENCES "company" ("id");

