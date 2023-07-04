import { getLines, clearLines } from "../queries/line.queries.js";
import { saveLinesToSale, createSale } from "../queries/sale.queries.js";
import * as invoiceHelper from "../helpers/Invoice.helper.js";
import { getItemsNameSku } from "../queries/item.queries.js";

export const getSalePage = async (req, res, next) => {
    try {
        const [lines, items] = await Promise.all([getLines(), getItemsNameSku()]);
        const invoice = await invoiceHelper.getInvoice(lines);
        res.render("sale", { lines, invoice, items, error: req.flash("error") });
    } catch (error) {
        next(error);
    }
};

export const finalizeSale = async (req, res, next) => {
    try {
        const lines = await getLines();
        const invoice = await invoiceHelper.getInvoice(lines);
        const sale = await createSale(lines, invoice);
        await saveLinesToSale(sale);
        await clearLines();
        res.redirect("/sale");
    } catch (error) {
        next(error);
    }
};
