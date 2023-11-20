#!/bin/bash

# Ange ditt födelsedatum
birthday="2023-11-20"

# Hämta dagens datum
current_date=$(date +%Y-%m-%d)

# Jämför dagens datum med födelsedatum
if [ "$current_date" == "$birthday" ]; then
    echo "🎉 Grattis! Det är din födelsedag idag!"
else
    echo "Idag är inte din födelsedag. 😊"
fi
