// Initializes the `address` service on path `/address`
const createService = require('feathers-sequelize');
const createModel = require('../../models/address.model');
const hooks = require('./address.hooks');

module.exports = function (app) {
  const Model = createModel(app);
  //const paginate = app.get('paginate');

  const options = {
    name: 'address',
    Model
  };

  // Initialize our service with any options it requires
  app.use('/address', createService(options));

  // Get our initialized service so that we can register hooks and filters
  const service = app.service('address');

  service.hooks(hooks);
};
