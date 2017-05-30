QUnit.module("Voting", function () {

	QUnit.module("voteFailed()", function () {
		QUnit.test("Removes the element with a matching ID when not votable", function (assert) {
			// Arrange
            var fixture = $("#qunit-fixture");
            fixture.append("<div id='1'>hello!</div>");
            var xhr = { responseJSON: { issueId: 1, isVotable: false } };

            // Verify DIV was added
            assert.equal($("div", fixture).length, 1, "div added successfully!");

            // Act
            voteFailed(xhr);

            //Assert
            assert.equal($("div", fixture).length, 0, "div removed");
        });
        
        QUnit.test("Leaves the div available if votable", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            fixture.append("<div id='1'></div>");
            var xhr = { responseJSON: { issueId: 1, isVotable: true } };

            // Verify DIV was added
            assert.equal($("div", fixture).length, 1, "div added successfully!");

            // Act
            voteFailed(xhr);

            //Assert
            assert.equal($("div", fixture).length, 1, "div not removed");
        });

        QUnit.test("Removes only the div with the given ID", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            fixture.append("<div id='1'></div>");
            fixture.append("<div id='2'></div>");
            fixture.append("<div id='3'></div>");
            fixture.append("<div id='4'></div>");
            // Verify DIVs were added
            assert.equal($("div", fixture).length, 4, "divs added successfully!");

            var xhr = { responseJSON: { issueId: 3, isVotable: false } };

            // Act
            voteFailed(xhr);

            //Assert
            assert.equal($("div", fixture).length, 3, "div not removed");

            assert.equal($("div", fixture)[0].id, 1);
            assert.equal($("div", fixture)[1].id, 2);
            assert.equal($("div", fixture)[2].id, 4);
        });

        QUnit.test("Leaves all DIVs in the DOM if no ID is passed", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            fixture.append("<div id='1'>hello!</div>");
            fixture.append("<div id='2'>hello!</div>");
            fixture.append("<div id='3'>hello!</div>");
            var xhr = { responseJSON: { isVotable: false } };

            // Verify DIV was added
            assert.equal($("div", fixture).length, 3, "div added successfully!");

            // Act
            voteFailed(xhr);

            //Assert
            assert.equal($("div", fixture).length, 3, "no divs removed");
        });

        QUnit.test("Leaves all DIVs in the DOM if no params are passed", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            fixture.append("<div id='1'>hello!</div>");
            fixture.append("<div id='2'>hello!</div>");
            fixture.append("<div id='3'>hello!</div>");
            var xhr = { responseJSON: { issueId: null, isVotable: null } };

            // Verify DIV was added
            assert.equal($("div", fixture).length, 3, "div added successfully!");

            // Act
            voteFailed(xhr);

            //Assert
            assert.equal($("div", fixture).length, 3, "no divs removed");
        });
	});
});