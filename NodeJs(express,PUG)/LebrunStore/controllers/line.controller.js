import { getLines, findAndAdd, saveNewLine, findExistingLine, removeItemFromLines, findAndUpdate, createNewLine } from "../queries/line.queries.js";
import { getItemBySku } from "../queries/item.queries.js";

export const addItemToLines = async (req, res, next) => {
    const { sku, quantity } = req.body;

    try {
        const newItem = await getItemBySku(sku);

        if (!newItem) {
            const error = new Error(`Veuillez choisir un article valide.`);
            error.status = 400;
            throw error;
        }

        if (quantity < 1) {
            const error = new Error(`Veuillez choisir une quantité valide.`);
            error.status = 400;
            throw error;
        }

        const lines = await getLines();
        const existingLine = findExistingLine(lines, sku);

        if (existingLine) {
            await findAndAdd(sku, quantity);
        } else {
            const line = createNewLine(newItem, quantity);
            await saveNewLine(line);
        }

        res.redirect("/sale");
    } catch (error) {
        next(error);
    }
};

export const deleteItemFromLines = async (req, res, next) => {
    const { sku } = req.body;

    try {
        await removeItemFromLines(sku);
        res.redirect("/sale");
    } catch (error) {
        next(error);
    }
};

export const updateQuantity = async (req, res, next) => {
    const { sku, quantity } = req.body;

    try {
        await findAndUpdate(sku, quantity);
        res.redirect("/sale");
    } catch (error) {
        next(error);
    }
};
