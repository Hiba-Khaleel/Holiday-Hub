# HolidayHub

### Documentation Link:
https://thorengruppen-my.sharepoint.com/:w:/g/personal/peter_svensson_student_nbi-handelsakademin_se/EdeEWxMgwLhOgnPcH50CzrYBMZi1a2qY6UqMQcq82dsQig?wdOrigin=TEAMS-MAGLEV.null_ns.rwc&wdExp=TEAMS-TREATMENT&wdhostclicktime=1732194805211&web=1

### Each person should:
- git clone
- (DESTINATION TO PROJECT ON GITHUB) EX: git@github.com:AlphaStackz/HolidayHub.git

### Preview:
- Utilizing Trello.com to divide user stories in to smaller task:

### Working with tasks:
- Choose a task from Trello.com
- Notify other teammates
- Create a feature-branch -> name as task
- *** IMPORTANT *** Pull for latest version
- Start working on the task
- When task is finished:
  - Move Task to review on Trello.com
  - Notify other teammates for code review
  - If task is big:
    - Code review with all teammates
  - If task is small or other tasks are dependant: 
    - Code review with at least one teammate
  - Big or small task:
    - git merge with main
    - git push
    - Notify teammates to pull the latest version of the code
    - delete branch from main using ->: git branch -d branch-name
    - move task to done in Trello.com

### Push new code
Check if anyone else is pushing and alert everyone:
1. git checkout main
2. git pull
3. git checkout BRANCHNAME
4. git add .
5. git commit -m "Write a description on what has been done"
6. git status
7. git push origin BRANCHNAME
8. git checkout main
9. git merge origin BRANCHNAME
   -- Ev resolve conflicts -->git add, git commit
10. git status            // Before pushing anything
11. git push


### Updates


