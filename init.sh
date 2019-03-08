#!/bin/bash

echo "Resetting database\n"
rm -f deathrace.db
dotnet ef database update
sqlite3 deathrace.db < seed_db.sql
