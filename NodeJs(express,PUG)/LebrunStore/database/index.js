import mongoose from "mongoose";

const protocol = "mongodb+srv";
const devUrl = "hkoncept.u1xfmf0.mongodb.net";
const prodUrl = "hkoncept.u1xfmf0.mongodb.net";
const params = "?retryWrites=true&w=majority";
const username = "User";
const password = "pw4User$";
const database = "1183706";
let url = devUrl;
if (process.env.NODE_ENV === "production") url = prodUrl;

const connectionString = `${protocol}://${username}:${password}@${url}/${database}${params}`;

const options = {
    useNewUrlParser: true,
    useUnifiedTopology: true,
};

export const connect = (callback) =>
    mongoose
        .connect(connectionString, options)
        .then(() => {
            console.log(`Connecté avec succès à la base ${database}`);
            if (callback) callback();
        })
        .catch((err) => {
            console.log(err);
        });
