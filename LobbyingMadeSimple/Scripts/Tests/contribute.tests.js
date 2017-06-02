QUnit.module('Contribute', function () {
    QUnit.module('validateValue() invalid data', function (hooks) {
        var $fixture;

        hooks.beforeEach(function (assert) {
            // Arrange
            $fixture = $('#qunit-fixture');
            $fixture.append('<span id="error-catcher"></span>');
            $fixture.append('<input id="amount" type="text" value="0" />');
            assert.equal($('input', $fixture).length, 1, 'Input added successfully');
        });

        QUnit.test('Adds an error message paragraph', function (assert) {
            // Act
            validateValue();

            // Assert
            assert.equal($('.error-text', $fixture).length, 1, 'Error text exists');
        });

        QUnit.test('Displays an error message inside the error paragraph', function (assert) {
            // Act
            validateValue();

            // Assert
            assert.ok($('.error-text')[0].innerText, 'Error message is not blank');
        });

        QUnit.test('Doesn\'t add duplicate error paragraphs', function (assert) {
            // Act
            validateValue();
            validateValue();

            // Assert
            assert.equal($('.error-text', $fixture).length, 1, 'Only one error-text paragraph is present');
        });

        QUnit.test('Is called when contribute button is clicked', function (assert) {
            // Arrange
            $fixture.append('<button id="contribute-btn" />');
            bindEventListeners();

            // Act
            $('#contribute-btn').trigger('click');

            // Assert
            assert.equal($('.error-text', $fixture).length, 1, 'Error text exists');
        });
    });

    QUnit.module('validateValue() valid data', function (hooks) {
        var $fixture;
        var dropinCalled;

        hooks.before(function (assert) {
            addBraintreeDropin = function () {
                dropinCalled = true;
            };
        });

        hooks.beforeEach(function (assert) {
            dropinCalled = false;

            // Assert
            $fixture = $('#qunit-fixture');
            $fixture.append('<input id="amount" type="text" value="3" />');
            assert.equal($('input', $fixture).length, 1, 'Input added successfully');
        });

        QUnit.test('Does not add an error if the value is >= 0', function (assert) {
            // Act
            validateValue();

            // Assert
            assert.equal($('.error-text', $fixture).length, 0, 'Error text does not exist');
        });

        QUnit.test('Calls addDropBraintreeDropin', function (assert) {
            // Verify bool is false
            assert.notOk(dropinCalled, 'Dropin not yet called');

            // Act
            validateValue();

            // Assert
            assert.ok(dropinCalled, 'Dropin boolean set to true');
        });

        QUnit.test('Adds a hidden class to the contribute-btn', function (assert) {
            // Arrange
            $fixture.append('<button id="contribute-btn"></button>');

            // Act
            validateValue();

            // Assert
            assert.ok($('#contribute-btn').hasClass('hidden'), 'Contribute-btn is hidden');
        });
        
        QUnit.test('Passes for values with commas', function (assert) {
            // Arrange
            $('#amount').remove();
            assert.equal($('input', $fixture).length, 0, 'Input removed successfully');
            $fixture.append('<input id="amount" value="1,000,000" />');
            assert.equal($('input', $fixture).length, 1, 'Input added successfully');

            // Act
            validateValue();

            // Assert
            assert.ok(dropinCalled, 'addBraintreeDropin called');
        });
    });
});