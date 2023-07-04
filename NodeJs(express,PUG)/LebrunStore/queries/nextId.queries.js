import NextId from "../database/models/nextid.model.js";

const createNextId = async (firstId) => {
    const nextId = new NextId({
        nextId: firstId,
    });
    await nextId.save();
};
export const getNextId = async () => {
    const nextId = await NextId.findOne();
    if (!nextId) {
        await createNextId(1000);
        return 1000;
    }
    return nextId.nextId;
};

export const incrementNextId = async () => {
    const nextId = await NextId.findOne();
    nextId.nextId++;
    await nextId.save();
};
