// Initializes the `phone` service on path `/phone`
const createService = require('feathers-sequelize');
const createModel = require('../../models/phone.model');
const hooks = require('./phone.hooks');

module.exports = function (app) {
  const Model = createModel(app);
  //const paginate = app.get('paginate');

  const options = {
    name: 'phone',
    Model
  };

  // Initialize our service with any options it requires
  app.use('/phone', createService(options));

  // Get our initialized service so that we can register hooks and filters
  const service = app.service('phone');

  service.hooks(hooks);
};
