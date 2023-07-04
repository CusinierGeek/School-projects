import Sale from "../database/models/sale.model.js";
import { getNextId, incrementNextId } from "./nextId.queries.js";

export const createSale = async (lines, invoice) => {
    const nextId = await getNextId();

    const sale = new Sale({
        id: nextId,
        lines: lines,
        subtotal: invoice.subTotal,
        gst: invoice.gst,
        qst: invoice.qst,
        total: invoice.total,
    });
    return sale;
};

export const saveLinesToSale = (sale) => {
    return sale.save().then(() => {
        incrementNextId();
    });
};
