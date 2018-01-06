// Initializes the `person` service on path `/person`
const createService = require('feathers-sequelize');
const createModel = require('../../models/person.model');
const hooks = require('./person.hooks');

module.exports = function (app) {
  const Model = createModel(app);
  const paginate = app.get('paginate');

  const options = {
    name: 'person',
    Model,
    paginate
  };

  // Initialize our service with any options it requires
  app.use('/person', createService(options));

  // Get our initialized service so that we can register hooks and filters
  const service = app.service('person');

  service.hooks(hooks);
};
