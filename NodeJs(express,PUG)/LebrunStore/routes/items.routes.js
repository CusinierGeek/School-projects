import express from "express";
import { getItem, uploadImage, addItem, removeItem, showItemsByPage, update, showUpdateItem } from "../controllers/item.controller.js";

const router = express.Router();

router.get("/", async (req, res, next) => {
    res.redirect("/items/1");
});
router.get("/sku/:sku", getItem);
router.post("/", addItem);
router.delete("/:sku", removeItem);
router.post("/upload", uploadImage);
router.get("/update/:sku", showUpdateItem);
router.post("/update", update);
router.get("/:page", showItemsByPage);

export default router;
