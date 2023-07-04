import Line from "../database/models/line.model.js";

export const getLines = () => {
    return Line.find().exec();
};

export const findAndUpdate = (sku, quantity) => {
    return Line.findOneAndUpdate({ sku: sku }, { quantity: quantity }).exec();
};

export const findAndAdd = (sku, quantity) => {
    return Line.findOneAndUpdate({ sku: sku }, { $inc: { quantity: quantity } }).exec();
};

export const removeItemFromLines = (sku) => {
    Line.deleteOne({ sku: sku }).exec();
};

export const saveNewLine = (newLine) => {
    return newLine.save();
};

export const findExistingLine = (lines, sku) => {
    return lines.find((line) => line.sku === sku);
};

export const createNewLine = (item, quantity) => {
    const newLine = new Line({
        sku: item.sku,
        name: item.name,
        sale_price: item.sale_price,
        quantity: quantity,
        image_url: item.image_url,
    });
    return newLine;
};
export const clearLines = () => {
    return Line.deleteMany().exec();
};
