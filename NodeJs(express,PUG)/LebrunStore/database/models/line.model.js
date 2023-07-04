import mongoose from "mongoose";

export const schema = mongoose.Schema({
    sku: {
        type: String,
        required: true,
        unique: true,
    },
    name: { type: String, required: true, max: 125 },
    sale_price: {
        type: Number,
        required: true,
        min: 0,
    },
    quantity: { type: Number, required: true, min: 0 },
    image_url: { type: String, required: true },
});

schema.virtual("amount").get(function () {
    return this.sale_price * this.quantity;
});

export default mongoose.model("Line", schema);
