import mongoose from "mongoose";

const schema = mongoose.Schema({
    sku: {
        type: String,
        required: { value: true, message: "SKU required" },
        unique: true,
        validate: {
            validator: function (value) {
                return value.length == 7;
            },
            message: "SKU must be 7 characters long",
        },
    },
    name: { type: String, required: { value: true, message: "Name required" }, max: 125 },
    description: { type: String, max: 3000 },
    sale_price: {
        type: Number,
        required: { value: true, message: "Price required" },
        min: 0,
        validate: {
            validator: function (value) {
                return value !== undefined;
            },
            message: "Sale price must be defined",
        },
    },
    image_url: { type: String },
    brand: { type: String, required: { value: true, message: "Brand required" }, min: 2, max: 30 },
});

export default mongoose.model("Item", schema);
