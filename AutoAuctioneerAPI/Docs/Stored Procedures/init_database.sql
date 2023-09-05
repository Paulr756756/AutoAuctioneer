\set ON_ERROR_STOP on

create table "users" (
    id uuid primary key,
    userName varchar(25) not null,
    serialNo serial,
    email varchar(50) unique not null,
    verificationToken text,
    verifiedAt timestamp,
    passwordResetToken text,
    resetTokenExpires timestamp,
    address text,
    registeredAt timestamp not null,
    phoneNo text,
    firstName varchar(15),
    lastName varchar(15),
    dateOfBirth date,
    passwordHash text
);

create table "items"(
    id uuid primary key,
    userId uuid references "users"(id) on delete cascade,
    type int not null
);

create table "cars" (
    id uuid primary key references "items"(id) on delete cascade,
    serialNo serial,
    make text not null,
    model text not null,
    year date,
    vin text,
    color varchar(20),
    bodyType varchar(30),
    fuelType varchar(20),
    transmissionType varchar(30),
    engineType varchar(20),
    horsepower int,
    torque int,
    fuelEfficiency real,
    acceleration real,
    topSpeed int,
    imageUrls text[]
);

create table "cars_features" (
    id uuid primary key references "cars" (id) on delete cascade, 
    seatingMaterial text,
    entertainmentSystem text,
    climateControl text,
    safetyFeatures text[],
    driverAssistanceFeatures text[]
);

create table "cars_technology_comfort" (
    id uuid primary key references "cars"(id) on delete cascade,
    infotainmentNavigationSystem text,
    bluetoothConnectivity text,
    usbPortsCharging text,
    interiorLighting text,
    adjustableSeatsSteeringWheel text,
    interiorStorageCompartments text
);

create table "cars_exterior_style" (
    id uuid primary key references "cars"(id) on delete cascade,
    exteriorDesign text,
    roofStyle text,
    wheelTypeSize text,
    exteriorLighting text
);

create table "cars_performance_handling"(
    id uuid primary key references "cars"(id) on delete cascade,
    suspensionType text,
    brakingSystem text,
    ecoFriendlyFeatures text,
    drivingModes text
);

create table "cars_space_practicality" (
    id uuid primary key references "cars"(id) on delete cascade,
    vehicleDimensions text,
    cargoSpaceStorage text,
    roofRackCompatible boolean
);

create table "cars_fuel_maintenance" (
    id uuid primary key references "cars"(id) on delete cascade,
    fuelEfficiency text,
    emissionsRating text,
    maintenanceSchedule text,
    warrantyDetails text,
    previousOwnersNo int,
    ownershipHistory text[],
    hasAccidentHistory boolean not null,
    serviceRecords text[]
);

create table "cars_customization_upgrades" (
    id uuid primary key references "cars"(id) on delete cascade,
    optionalPackages text,
    individualUpgrades text,
    additionalAccessories text[]
);


create table "parts" (
    id uuid primary key references "items"(id) on delete cascade,
    name text,
    description text,
    category text,
    marketPrice bigint,
    partType int not null,
    manufacturer text
);

create table "parts_engine" (
    id uuid primary key references "parts"(id) on delete cascade,
    engineType text not null,
    displacement real,
    horsepower int,
    torque int
);

create table "parts_specific"(
    id uuid primary key references "parts"(id) on delete cascade,
    carMake text not null,
    carModel text not null,
    year date
);

create table "listings"(
    id uuid primary key,
    userId uuid references "users"(id) on delete cascade,
    itemId uuid references "items"(id) on delete cascade
);

create table "bids"(
    id uuid primary key,
    userId uuid references "users"(id) on delete cascade,
    listingId uuid references "listings"(id) on delete cascade,
    bidAmount bigint not null,
    bidTime timestamp not null
);

