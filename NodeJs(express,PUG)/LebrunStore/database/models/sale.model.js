import mongoose from "mongoose";

const schema = mongoose.Schema(
    {
        id: {
            type: Number,
            required: true,
            min: 1,
            unique: true,
        },

        lines: { type: Array },
        subtotal: {
            type: Number,
            required: true,
            min: 0,
            validate: {
                validator: function (value) {
                    return value !== undefined;
                },
            },
        },
        qst: {
            type: Number,
            required: true,
            min: 0,
        },
        gst: {
            type: Number,
            required: true,
            min: 0,
        },
        total: {
            type: Number,
            required: true,
            min: 0,
        },
    },
    { timestamps: true }
);

export default mongoose.model("Sale", schema);
