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

    QUnit.module("removeVoteButtonColors()", function () {
        QUnit.test("Removes all bootstrap btn-success, btn-primary, btn-defaults from all nested vote-btn's", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var input = "<input class='btn-success btn-primary btn-danger vote-btn' />";
            fixture.append("<div id='1'>" + input + input + "</div>");

            // Verify DIV was added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 2, "inputs added successfully!");

            // Act
            removeVoteButtonColors("#1 .vote-btn");

            //Assert
            $("input", fixture).each(function (i, elem) {
                assert.equal(elem.classList, "vote-btn", "Class lists match");
            });
        });

        QUnit.test("Doesn't remove anything from non-vote-buttons", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var input = "<input class='btn-success' />";
            fixture.append("<div id='1'>" + input + input + "</div>");

            // Verify DIV was added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 2, "inputs added successfully!");

            // Act
            removeVoteButtonColors("#1 .vote-btn");

            //Assert
            $("input", fixture).each(function (i, elem) {
                assert.equal(elem.classList, "btn-success", "Class lists match");
            });
        });

        QUnit.test("Doesn't remove anything when called with null", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var input = "<input class='btn-success' />";
            fixture.append("<div id='1'>" + input + input + "</div>");

            // Verify DIV was added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 2, "inputs added successfully!");

            // Act
            removeVoteButtonColors(null);

            //Assert
            $("input", fixture).each(function (i, elem) {
                assert.equal(elem.classList, "btn-success", "Class lists match");
            });
        });
    });

    QUnit.module("removePercentageColors()", function () {
        QUnit.test("Removes Bootstrap text colors success and failure when passed a valid selector string", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var span = "<span class='vote-percentage-string text-success'>67</span>";
            fixture.append("<div id='1'>" + span + "</div>");
            var selector = "#1 .vote-percentage-string";

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("span", fixture).length, 1, "span added successfully!");

            // Act
            removePercentageColors(selector);

            //Assert
            $("span", fixture).each(function (i, elem) {
                assert.equal(elem.classList, "vote-percentage-string", "Class lists match");
            });
        });

        QUnit.test("Returns a chainable JQuery object", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            fixture.append("<div id='1'></div>");
            var selector = "#1";

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");

            // Act
            result = removePercentageColors(selector)
                .addClass("test");

            //Assert
            assert.equal(result[0].classList, "test", "Object returned is JQuery chainable");
        });
    });

    QUnit.module("changePercentageColors()", function () {
        QUnit.test("Adds a new css class to a given selector", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var span = "<span class='vote-percentage-string text-success'>67</span>";
            fixture.append("<div id='1'>" + span + "</div>");
            var selector = "#1 .vote-percentage-string";

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("span", fixture).length, 1, "span added successfully!");

            // Act
            changePercentageColors(selector, "text-danger");

            //Assert
            assert.ok($(".vote-percentage-string").hasClass("text-danger"));
            assert.notOk($(".vote-percentage-string").hasClass("text-success"));
        });

        QUnit.test("Only affects the given selector strings", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var span = "<span class='vote-percentage-string text-success'>67</span>";
            var span2 = "<span class='percentage-string text-success'>67</span>";
            fixture.append("<div id='1'>" + span + span2 + "</div>");
            var selector = "#1 .vote-percentage-string";

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("span", fixture).length, 2, "spans added successfully!");

            // Act
            changePercentageColors(selector, "text-danger");

            //Assert
            assert.ok($(".vote-percentage-string").hasClass("text-danger"));
            assert.notOk($(".vote-percentage-string").hasClass("text-success"));
            assert.ok($(".percentage-string").hasClass("text-success"));
        });
    });


    QUnit.module("colorVoteButton()", function () {
        QUnit.test("Adds a btn-success class to a vote-btn based on the response", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var upBtn = "<input class='up-vote' />";
            fixture.append("<div id='1'>" + upBtn + "</div>");

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 1, "inputs added successfully!");

            // Act
            colorVoteButton(true, "#1");

            //Assert
            assert.ok($(".up-vote").hasClass("btn-success"), "up-vote has btn-success class");
        });

        QUnit.test("Adds a btn-danger class to a vote-btn based on the response", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var downBtn = "<input class='down-vote' />";
            fixture.append("<div id='1'>" + downBtn + "</div>");

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 1, "inputs added successfully!");

            // Act
            colorVoteButton(false, "#1");

            //Assert
            assert.ok($(".down-vote").hasClass("btn-danger"), "down-vote has btn-danger class");
        });

        QUnit.test("Only affects one input per call", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var upBtn = "<input class='up-vote' />";
            var downBtn = "<input class='vote' />";
            fixture.append("<div id='1'>" + upBtn + downBtn + "</div>");

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 2, "inputs added successfully!");

            // Act
            colorVoteButton(true, "#1");

            //Assert
            assert.ok($(".up-vote").hasClass("btn-success"), "up-vote has btn-success class");
            assert.notOk($(".down-vote").hasClass("btn-danger"), "down-vote does not have btn-danger");
        });

        QUnit.test("Doesn't affect non up-vote or down-vote classes inputs", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var downBtn = "<input class='vote' />";
            fixture.append("<div id='1'>" + downBtn + "</div>");

            // Verify elements were added
            assert.equal($("div", fixture).length, 1, "div added successfully!");
            assert.equal($("input", fixture).length, 1, "inputs added successfully!");

            // Act
            colorVoteButton(false, "#1");

            //Assert
            assert.notOk($(".vote").hasClass("btn-danger"), "vote does not have color class");
        });
        
        QUnit.test("Finds the button even if nested deeply", function (assert) {
            // Arrange
            var fixture = $("#qunit-fixture");
            var downBtn = "<input class='up-vote' />";
            fixture.append("<div><div id='1'><div><div><div>" + downBtn + "</div></div></div></div></div>");

            // Verify elements were added
            assert.equal($("div", fixture).length, 5, "div added successfully!");
            assert.equal($("input", fixture).length, 1, "input added successfully!");

            // Act
            colorVoteButton(true, "#1");

            //Assert
            assert.ok($(".up-vote").hasClass("btn-success"), "up-vote has btn-success class");
        });
    });
});