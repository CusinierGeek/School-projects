import { Router } from "express";
import { addItemToLines, deleteItemFromLines, updateQuantity } from "../controllers/line.controller.js";

const router = Router({ mergeParams: true });

router.post("/", addItemToLines);
router.post("/delete", deleteItemFromLines);
router.post("/update", updateQuantity);

export default router;
