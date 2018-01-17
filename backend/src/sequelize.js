require('dotenv').config();
const Sequelize = require('sequelize');

module.exports = function (app) {
  const sequelize = new Sequelize(process.env.PGDBNAME, process.env.PGUSERNAME, process.env.PGPASS, {
    host: process.env.PGHOST, 
    port: process.env.PGPORT,
    dialect: 'postgres',
    logging: false, 
    operatorsAliases: Sequelize.Op,
    define: {
      freezeTableName: true
    }
  });
  const oldSetup = app.setup;

  app.set('sequelizeClient', sequelize);

  app.setup = function (...args) {
    const result = oldSetup.apply(this, args);

    // Set up data relationships
    const models = sequelize.models;
    Object.keys(models).forEach(name => {
      if ('associate' in models[name]) {
        models[name].associate(models);
      }
    });

    // Sync to the database
    sequelize.sync();

    return result;
  };
};
