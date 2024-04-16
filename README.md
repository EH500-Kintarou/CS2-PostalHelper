# PostalHelper

Mod helps the post facilities to work more smoothly.
- **Post Office** gets Mail to Deliver when in critical need.
- **Post Sorting Office** gets Unsorted Mail when in critical need.
If your postal infrastructure is working good and efficient then this mod will simply do nothing :)

### Rationale

The dreaded **Unrealiable mail service** happiness malus happens when there is a lack of Mail to Deliver in post offices. Collecting mail is not enough to get rid of this malus. Post Vans need to deliver mail regularly. Mail to Deliver is supposed to be transferred either from Cargo Stations or from Post Sorting Facilities (and they get it by sorting Unsorted Mail). The mail transfer processes seems to be malfunctioning from time to time e.g. there can be hundreds of tons of Mail to Deliver in Cargo stations and yet the post offices are lacking it. The mod basically simulates the mail transfer in critical cases, see below.

### Features

The mod checks every 45 in-game minutes if post facilities have enough mail to process.
- **Post Office** gets 4000 units of Mail to Deliver (2 Post Vans) if its level of Mail to Deliver drops below 4000 units.
- **Post Sorting Facility** gets 25000 units of Unsorted Mail (1 Truck) if its level of Unsorted Mail drops below 25000 units.
Each time the mod adds some mail, there will be an entry in the log. When there are no entries it means that your postal infrastructure is performing efficient.

### Technical

- This is a workaround solution until CO will fix the mail transfer processes.
- The mod does **not** modify savefiles.
- The mod does **not** modify any game systems.
