import Tax from "../database/models/tax.model.js";

export const setTaxes = async (qst, gst) => {
    try {
        await Tax.deleteMany({});
        const tax = new Tax({
            name: "GST",
            rate: gst,
        });
        const tax2 = new Tax({
            name: "QST",
            rate: qst,
        });
        await tax.save();
        await tax2.save();
    } catch (error) {
        console.log(error);
    }
};

export const getTaxes = async () => {
    try {
        const taxes = await Tax.find({});
        return taxes;
    } catch (error) {
        console.log(error);
    }
};
