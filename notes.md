# Notes

## Petri Net

* Implictly one-way

## DCR

* Talk about accepting semantics

### Our model

* Could have had timed cond/resp with 1. on every next step
  * Problem with conditionals
  * Note: talk about self-conditioned pending (Heirarch blah, fig 6.)
* Need to add (CHANGE PHASE TO ABANDON -->O EXECUTE ABANDON) <!-- not needed
* WOO

### DCR Process Mining

* Our project
  * Have a graph
  * Deduced runs from it
  * Checked if traces from Dreyers log were accepting

### Dead/livelock

* Deadlock
  * Run is not accepting but no events are executable
* Livelock
  * Can execute events infinitely but never reach accepting state
    * NOTE: a pending loop is NOT livelocked because we remove pendings before adding new pendings

### Timed DCR

* Delays
  * Condition
  * Describes minimum timesteps to wait before execution of target
* deadlines (response)
  * Response
  * Describes maximum timesteps allowed before execution of target
* Timelock
  * Exists!

### Data in DCR

* It exists?

## Ass3

* Complementary ports: input/output
* in Buyer:
  * outputports: n
    * to discriminate between sellers
    * ideally we should have had just 1 to follow the CCS interface diagram, discriminate in another way
  * inputports: 1
    * All sellers send to the same input port
    * We ask 1 seller at a time to discriminate between them
* Could have written ask()() as ResponseRequest, then we could have them all running concurrently
  * we have it OneWay so we need to run sequentially
* 

## Choreographies

* cannot specify liveness
* iterative
* all splits have to go into their own swimlanes
* end point projection
  * Criteria:
    * the initiator of an activity needs to have been involved in the direct previous activity