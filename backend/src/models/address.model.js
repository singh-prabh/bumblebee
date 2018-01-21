// See http://docs.sequelizejs.com/en/latest/docs/models-definition/
// for more of what you can do here.
const Sequelize = require('sequelize');
const DataTypes = Sequelize.DataTypes;

module.exports = function (app) {
  const sequelizeClient = app.get('sequelizeClient');
  const address = sequelizeClient.define('address', {
    personID: {
      type: DataTypes.INTEGER
    }, 
    companyID: {
      type: DataTypes.INTEGER
    }, 
    street1: {
      type: DataTypes.STRING  
    }, 
    street2: {
      type: DataTypes.STRING
    }, 
    city: {
      type: DataTypes. STRING
    },
    zip: {
      type: DataTypes.STRING
    }, 
    stateID: {
      type: DataTypes.INTEGER
    },
    typeID: {
      type: DataTypes.INTEGER
    }, 
    createdAt: {
      type: DataTypes.DATE
    },
    updatedAt: {
      type: DataTypes.DATE
    }
  }, {
    hooks: {
      beforeCount(options) {
        options.raw = true;
      }
    }
  });

  address.associate = function (models) { // eslint-disable-line no-unused-vars
    // Define associations here
    // See http://docs.sequelizejs.com/en/latest/docs/associations/
  };

  return address;
};
