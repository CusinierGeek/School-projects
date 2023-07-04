import * as db from "../database/index.js";
import fs from "fs";
import Item from "../database/models/item.model.js";

const ITEM_FILE = "./data/items.json";
const rawItems = JSON.parse(fs.readFileSync(ITEM_FILE, "utf8"));
const items = rawItems.filter((item, index, self) => !self.slice(index + 1).some((i) => i.sku === item.sku));

// Appeler la fonction de connexion pour établir la connexion à la base de données
db.connect(() => {
    const totalItems = items.length;
    let importedItems = 0;
    // Supprimer tous les documents de la collection avant de commencer l'importation
    Item.deleteMany({})
        .then(() => {
            console.log("Tous les items ont été supprimés. Importation des items...");

            items.forEach((itemData) => {
                const newItem = new Item({
                    sku: itemData.sku,
                    name: itemData.name,
                    description: itemData.description,
                    sale_price: itemData.sale_price,
                    image_url: itemData.image_url,
                    brand: itemData.brand,
                });

                newItem
                    .save()
                    .then(() => {
                        importedItems++;
                        const progressPercentage = (importedItems / totalItems) * 100;
                        process.stdout.clearLine();
                        process.stdout.cursorTo(0);
                        process.stdout.write(`${progressPercentage.toFixed(0)}% effectués`);
                    })
                    .catch((error) => {
                        console.error("Error saving item", error);
                    })
                    .finally(() => {
                        if (importedItems === totalItems) {
                            console.log("\nImportation terminée !");
                            process.exit(0);
                        }
                    });
            });
        })
        .catch((error) => {
            console.error("Error deleting documents", error);
        });
});
