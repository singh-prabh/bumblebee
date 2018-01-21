const assert = require('assert');
const app = require('../../src/app');

describe('\'phone\' service', () => {
  it('registered the service', () => {
    const service = app.service('phone');

    assert.ok(service, 'Registered the service');
  });
});
