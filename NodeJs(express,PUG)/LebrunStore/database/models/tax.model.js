import mongoose from "mongoose";

const taxSchema = mongoose.Schema({
    name: String,
    rate: Number,
});

export default mongoose.model("Tax", taxSchema);