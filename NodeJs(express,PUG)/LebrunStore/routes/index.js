import { Router } from "express";
import saleRoutes from "./sale.routes.js";
import itemsRoutes from "./items.routes.js";
import lineRoutes from "./line.routes.js";

const router = Router();

router.use("/sale", saleRoutes);
router.use("/line", lineRoutes);
router.use("/items", itemsRoutes);

router.get("/", (req, res) => {
    res.render("home");
});

router.get("*", (req, res) => res.redirect("/"));

export default router;
