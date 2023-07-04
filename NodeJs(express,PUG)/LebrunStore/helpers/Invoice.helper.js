import { getTaxes } from "../queries/tax.queries.js";

const getTaxesRate = async () => {
    const taxes = await getTaxes();
    const gst = taxes.find((tax) => tax.name === "GST").rate;
    const qst = taxes.find((tax) => tax.name === "QST").rate;
    return {
        taxes: {
            gst: gst,
            qst: qst,
        },
    };
};

const calculateSubTotal = (lines) => {
    let subTotal = 0;
    lines.forEach((line) => {
        subTotal += line.sale_price * line.quantity;
    });
    return parseFloat(subTotal.toFixed(2));
};

const calculateGst = async (lines) => {
    const config = await getTaxesRate();
    const subTotal = calculateSubTotal(lines);
    const gst = (subTotal * config.taxes.gst).toFixed(2);
    return gst;
};

const calculateQst = async (lines) => {
    const config = await getTaxesRate();
    const subTotal = calculateSubTotal(lines);
    const qst = (subTotal * config.taxes.qst).toFixed(2);
    return qst;
};

export const getInvoice = async (lines) => {
    const gst = await calculateGst(lines);
    const qst = await calculateQst(lines);
    const subTotal = calculateSubTotal(lines);
    const total = (subTotal + parseFloat(gst) + parseFloat(qst)).toFixed(2);

    return {
        subTotal: subTotal,
        gst: gst,
        qst: qst,
        total: total,
    };
};
