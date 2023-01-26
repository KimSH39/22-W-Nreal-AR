'use strict'

const express = require('express')
const bodyParser = require('body-parser')
const SmartApp = require('@smartthings/smartapp')

const server = (module.exports = express())
server.use(bodyParser.json())

const app = new SmartApp()

/* Only here for Glitch, so that GET doesn't return an error */
server.get('/', (req, res) => {
  res.send('Simple SmartApp Example URL: https://' + req.hostname)
})

/* Handles lifecycle events from SmartThings */
server.post('/', async (req, res) => {
  app.handleHttpCallback(req, res)
})

/* Defines the SmartApp */
app
  .enableEventLogging() // Log and pretty-print all lifecycle events and responses
  .configureI18n() // Use files from locales directory for configuration page localization
  .page('mainPage', (context, page, configData) => {
    page.section('sensors', (section) => {
      section
        .deviceSetting('sensor') // User가 하나 또는 그 이상의 장치를 선택할 수 있는 컨트롤을 create
        .capabilities(['contactSensor']) // 장치에 필요한 기능 설정, 선택 목록에 표시하려면 장치에 지정된 모든 기능이 있어야 함
        .required(true)
    })
    page.section('lights', (section) => {
      section
        .deviceSetting('lights')
        .capabilities(['switch'])
        .multiple(true) // 이 장치 설정이 여러 값을 가질 수 있는지
        .permissions('rx')
    })
  })
  .updated(async (context, updateData) => {
    // Installation Events
    await context.api.subscriptions.unsubscribeAll()
    return Promise.all([
      context.api.subscriptions.subscribeToDevices(
        context.config.sensor,
        'contactSensor', // 'contactSensor'
        'contact.open', // 'contact'
        'openDeviceEventHandler', // 'deviceEventHandler'
      ),
      context.api.subscriptions.subscribeToDevices(
        context.config.sensor,
        'contactSensor',
        'contact.closed',
        'closedDeviceEventHandler',
      ),
    ])
  })

  // subscribed event에 대한 처리를 정의
  .subscribedEventHandler('openDeviceEventHandler', (context, deviceEvent) => {
    return context.api.devices.sendCommands(
      context.config.lights,
      'switch',
      'on',
    )
  })
  .subscribedEventHandler(
    'closedDeviceEventHandler',
    (context, deviceEvent) => {
      return context.api.devices.sendCommands(
        context.config.lights,
        'switch',
        'off',
      )
    },
  )

/* Starts the server */
let port = 3000
server.listen(port)
console.log(`Open: http://127.0.0.1:${port}`)
