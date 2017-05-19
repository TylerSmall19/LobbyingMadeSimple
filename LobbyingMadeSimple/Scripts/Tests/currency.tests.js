QUnit.module("Currency", function () {

    QUnit.module("addCommas()", function () {
        QUnit.test("Adds commas every 3 digits when valid", function (assert) {
            // Arrange
            var $mockFields = $([{ value: "1000" }, { value: "100000000000000000" }]);

            // Act
            addCommas($mockFields);

            //Assert
            assert.deepEqual($mockFields[0], { value: "1,000" });
            assert.deepEqual($mockFields[1], { value: "100,000,000,000,000,000" });
        });

        QUnit.test("Sets value to 0 when NaN", function (assert) {
            // Arrange
            var $mockField = $([{ value: "Sam" }]);

            // Act
            addCommas($mockField);

            // Assert
            assert.deepEqual($mockField[0], { value: 0 });
        });

        QUnit.test("Ignores letters when in the middle of a number", function (assert) {
            // Arrange
            var $mockField = $([{ value: "1000a00" }]);

            // Act
            addCommas($mockField);

            // Assert
            assert.deepEqual($mockField[0], { value: "1,000" });
        });
    });

    QUnit.module("stripCommas()", function () {

        QUnit.test("Removes commas from number strings when they have them", function (assert) {
            // Arrange
            var numberString = "1,000,000,000";
            var noCommaString = "1000000000";

            // Act
            var result = stripCommas(numberString);

            // Assert
            assert.equal(result, noCommaString);
        });

        QUnit.test("Doesn't remove anything when there are no commas", function (assert) {
            // Arrange
            var junkString = "!@#$%^&*()-=_+{}[]\\|'\";:<>?/1234";

            // Act
            var result = stripCommas(junkString); 

            // Assert
            assert.equal(result, junkString);
        });
    });

    QUnit.module("removeCommasFromFields()", function () {

        QUnit.test("Removes commas on an entire array of values", function (assert) {
            // Arrange
            var $mockFields = $([{ value: "1,000" }, { value: "100,000,000,000,000,000" }]);

            // Act
            removeCommasFromFields($mockFields);

            //Assert
            assert.deepEqual($mockFields[0], { value: "1000" });
            assert.deepEqual($mockFields[1], { value: "100000000000000000" });
        });
    });
});