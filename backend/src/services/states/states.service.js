// Initializes the `states` service on path `/states`
const createService = require('feathers-sequelize');
const createModel = require('../../models/states.model');
const hooks = require('./states.hooks');

module.exports = function (app) {
  const Model = createModel(app);
  //const paginate = app.get('paginate');

  const options = {
    name: 'states',
    Model
  };

  // Initialize our service with any options it requires
  app.use('/states', createService(options));

  // Get our initialized service so that we can register hooks and filters
  const service = app.service('states');

  service.hooks(hooks);
};
