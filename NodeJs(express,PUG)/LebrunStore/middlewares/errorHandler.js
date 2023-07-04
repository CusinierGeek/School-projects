export const errorHandler = (err, req, res, next) => {
    console.error(err);
    const errorMessage = err.message || "Une erreur s'est produite";
    if (err.status === 400) {
        req.flash("error", errorMessage);
        return res.redirect("/sale");
    }

     res.status(500).render("error", { errorMessage, status: 500 });
};
