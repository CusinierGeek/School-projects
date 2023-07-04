import Item from "../database/models/item.model.js";

export const getItemBySku = (sku) => {
    return Item.findOne({ sku: sku }).exec();
};

export const getItemsNameSku = () => {
    return Item.find({}, { name: 1, sku: 1 }).collation({ locale: "fr" }).sort({ name: 1 }).exec();
};

export const getItems = () => {
    return Item.find({}).collation({ locale: "fr" }).sort({ name: 1 });
};

export const createItem = (item) => {
    const newItem = new Item({
        sku: item.sku,
        name: item.name,
        sale_price: item.sale_price,
        brand: item.brand,
        description: item.description,
        image_url: item.image_url,
    });
    return newItem.save();
};

export const deleteItem = (sku) => {
    return Item.deleteOne({ sku: sku }).exec();
};

export const updateItem = (sku, item,check ) => {
    
    return Item.findOneAndUpdate({ sku: sku }, item, { new: true, runValidators: true ? check : false }).exec();
};




export const countItems = () => {
    return Item.countDocuments({}).exec();
}

