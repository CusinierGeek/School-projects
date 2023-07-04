import { getItemBySku, getItems, deleteItem, createItem, updateItem, countItems } from "../queries/item.queries.js";
import multer from "multer";
import path from "path";
import { v4 as uuidV4 } from "uuid";

const storage = multer.diskStorage({
    destination: path.join(process.cwd(), "/public/images"),
    filename: (req, file, callback) => {
        callback(null, uuidV4() + path.extname(file.originalname));
    },
});

const upload = multer({
    storage: storage,
    limits: {
        fileSize: 5 * 1024 * 1024, // Limite de taille du fichier à 5 Mb
        fieldNameSize: 50, // Limite de la taille du nom du champ à 50 caractères
    },
    fileFilter: (req, file, callback) => {
        const acceptedExtensions = [".jpg", ".jpeg", ".png"];
        const ext = path.extname(file.originalname).toLowerCase();
        if (acceptedExtensions.includes(ext)) {
            callback(null, true);
        } else {
            callback(new Error("Seules les images sont autorisées."));
        }
    },
}).single("image");

export const getItem = async (req, res) => {
    const sku = req.params.sku;

    try {
        const item = await getItemBySku(sku);
        res.render("item", { item });
    } catch (error) {
        res.redirect("/sale");
    }
};

export const showUpdateItem = async (req, res, next) => {
    try {
        const item = await getItemBySku(req.params.sku);
        res.send(item);
    } catch (error) {
        next(error);
    }
};

export const uploadImage = (req, res, next) => {
    upload(req, res, (error) => {
        if (error) {
            console.error(error);
            return res.status(400).json({ error: error.message });
        }

        if (req.file) {
            const imageUrl = "/public/images/" + req.file.filename.toLowerCase();
            res.status(200).send(imageUrl);
        } else {
            res.status(400).json({ error: error.message });
        }
    });
};

export const addItem = async (req, res, next) => {
    try {
        await createItem(req.body);
        res.sendStatus(200);
    } catch (error) {
        console.log(error);
        res.status(400).json({ error: error.message });
    }
};

export const removeItem = async (req, res, next) => {
    try {
        await deleteItem(req.params.sku);
        res.sendStatus(200);
    } catch (error) {
        next(error);
    }
};

export const showItemsByPage = async (req, res, next) => {
    const page = parseInt(req.params.page) || 1;
    const itemsPerPage = parseInt(req.query.itemsPerPage) || 10;
    const paginationSize = 7;

    try {
        let items, totalItems, totalPages, startPage, endPage;

        if (itemsPerPage === -1) {
            // Afficher tous les articles
            items = await getItems().exec();
            totalItems = items.length;
            totalPages = 1;
            startPage = 1;
            endPage = 1;
        } else {
            // Afficher les articles paginés
            totalItems = await countItems();

            startPage = Math.max(1, page - Math.floor(paginationSize / 2));
            endPage = Math.min(startPage + paginationSize - 1, Math.ceil(totalItems / itemsPerPage));

            if (endPage - startPage + 1 < paginationSize) {
                startPage = Math.max(1, endPage - paginationSize + 1);
            }

            let itemsQuery = getItems();

            itemsQuery = itemsQuery.skip((page - 1) * itemsPerPage).limit(itemsPerPage);

            items = await itemsQuery.exec();

            totalPages = Math.ceil(totalItems / itemsPerPage);
        }

        res.render("items", { items, page, totalPages, startPage, endPage, itemsPerPage });
    } catch (error) {
        next(error);
    }
};

export const update = async (req, res, next) => {
    const sku = req.body.sku;
    const item = req.body;
    try {
        if (item.image_url === "") {
            await updateItem(sku, item, false);
        } else {
            await updateItem(sku, item, true);
            res.sendStatus(200);
        }
    } catch (error) {
        res.status(400).json({ error: error.message });
    }
};
