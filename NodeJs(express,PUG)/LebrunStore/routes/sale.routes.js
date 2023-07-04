import { Router } from "express";
import { getSalePage, finalizeSale } from "../controllers/sale.controller.js";

const router = Router({ mergeParams: true });

router.get("/", getSalePage);
router.post("/finalisation", finalizeSale);

export default router;
