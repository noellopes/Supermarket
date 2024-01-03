# Remove Snapshot
echo ">>>>>> REMOVE SNAPSHOTS"
rm ./Data/Migrations/Application/*
rm ./Data/Migrations/Supermarket/*

# Add New Migration
echo ">>>>>> ADD INITIAL MIGRATION - ApplicationDbContext"
Add-Migration Initial -Context ApplicationDbContext -OutputDir ./Data/Migrations/Application

echo ">>>>>> ADD INITIAL MIGRATION - SupermarketDbContext"
Add-Migration Initial -Context SupermarketDbContext -OutputDir ./Data/Migrations/Supermarket

# Drop Database
echo ">>>>>> DROP DATABASE"
Drop-Database -Context ApplicationDbContext
Drop-Database -Context SupermarketDbContext

# Update Database
echo ">>>>>> UPDATE DATABASE"
Update-Database -Context ApplicationDbContext
Update-Database -Context SupermarketDbContext