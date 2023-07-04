import express from "express";
import router from "./routes/index.js";
import flash from "express-flash";
import session from "express-session";
import * as db from "./database/index.js";
import { setTaxes } from "./queries/tax.queries.js";
import { gst, qst } from "./config/tax.config.js";
import { errorHandler } from "./middlewares/errorHandler.js";
import path from "path";

const app = express();
const PORT = 3000;

// Middleware
app.use(express.static(path.join(process.cwd(), "/public")));
app.use(express.static(path.join(process.cwd(), "/public/images")));
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(
    session({
        secret: "chaton",
        resave: false,
        saveUninitialized: false,
    })
);

app.use(flash());
app.set("view engine", "pug");
app.use(router);
app.use(errorHandler);

(async () => {
    try {
        await db.connect();
        await setTaxes(qst, gst);
        app.listen(PORT, () => {
            console.log(`Server running at: http://localhost:${PORT}/`);
        });
    } catch (error) {
        console.error("Failed to start server:", error);
    }
})();
