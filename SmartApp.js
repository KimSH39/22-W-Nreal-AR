const SmartApp = require('@smartthings/smartapp')

async function setVolume(ctx) {
  const volume = await ctx.configNumberValue('audioVolume')

  await ctx.api.devices.sendCommands(ctx.config.monitor, [
    {
      capability: 'audioVolume',
      command: 'setVolume',
      arguments: [volume],
    },
  ])
}

/* Defines the SmartApp */
module.exports = new SmartApp()
  .enableEventLogging() // Log and pretty-print all lifecycle events and responses
  .configureI18n() // Use files from locales directory for configuration page localization
  .page('mainPage', (context, page, configData) => {
    page.section('control', (section) => {
      section.enumSetting('numSelect').options({ on: 'on', off: 'off' })
      section.numberSetting('audioVolume')
    })
    page.section('Samsung M5 (27)', (section) => {
      section
        .deviceSetting('monitor')
        .capabilities(['switch', 'audioVolume'])
        .permissions('rx')
        .required(true)
    })
  })

  .updated(async (ctx) => {
    const power = await ctx.configStringValue('numSelect')

    await ctx.api.schedules.delete()

    await setVolume(ctx)

    ctx.api.devices.sendCommands(ctx.config.monitor, 'switch', power)
  })
