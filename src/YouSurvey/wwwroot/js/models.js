var Survey = (function () {
    function Survey(id, name, createdBy, createdDate, lastUpdatedBy, lastUpdatedDate) {
        this.id = id;
        this.name = name;
        this.createdBy = createdBy;
        this.createdDate = createdDate;
        this.lastUpdatedBy = lastUpdatedBy;
        this.lastUpdatedDate = lastUpdatedDate;
    }
    return Survey;
}());