
import mongoose from "mongoose";

const nextIdSchema = new mongoose.Schema({
    nextId: {
        type: Number,
        default: 1000,
    },
});

export default mongoose.model("NextId", nextIdSchema);
