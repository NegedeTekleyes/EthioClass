# EthioClass App

## 🎓 EthioClass — School Management Companion for Ethiopian Private Schools

### 🌍 Vision

EthioClass is a school management platform designed specifically for Ethiopian private K-12 schools. It aims to close the gap in existing school software by offering **localized grading (Ethiopian mark system), Amharic language support, SMS-first parent communication, and Ethiopian calendar integration** in one unified platform.

Most global school-management tools are built for Western grading systems, English-only communication, and assume reliable parent email — none of which match how Ethiopian private schools actually operate. EthioClass bridges that gap.

### 🚀 Core Problem

Existing school record-keeping in Ethiopian private schools:

- Runs on paper registers or disconnected Excel sheets per class
- Requires manual, error-prone calculation of term averages and rankings
- Gives parents no way to check attendance or grades without visiting in person
- Relies on printed report cards that take days to prepare each term
- Has no reliable way to reach parents quickly (most rely on SMS, not email or apps)
- Loses fee-payment history across terms, making arrears hard to track

---

### 🎓 EthioClass Features

#### 1. 👩‍🎓 Student Record & Academic Journey (Core Experience)

A complete, always-current academic record for every student, replacing the paper register.

Includes:
- Student profile (bio data, guardian contacts, class/section)
- Subject enrollment per term
- Term-by-term grade history
- Attendance log, daily and cumulative
- Conduct/discipline notes
- "This Term's Summary" dashboard for admins and homeroom teachers

👉 This becomes the **main daily screen** for teachers and admins.

#### 2. 📅 Academic Calendar & Term Scheduling

Keeps the school on track across the Ethiopian academic year.

- Ethiopian calendar and Gregorian calendar shown side by side
- Term start/end dates, exam schedules
- Holiday and closure notices
- Automatic term rollover (new term = fresh grade sheet, same student record)

#### 3. 📝 Grading & Report Card Engine (Key Differentiator)

A grading system built around how Ethiopian schools actually grade, not adapted from a foreign template.

Each report card includes:
- Marks out of 100 per subject
- Term average and class rank
- Conduct grade
- Attendance summary for the term
- Teacher remarks (English and Amharic)
- Amharic and English name fields, printed side by side

Features:
- Auto-calculated term averages and rankings — no more manual spreadsheet math
- One-click PDF report card generation, ready to print
- Bulk generation for an entire class at once

#### 4. 💬 Parent Communication (Telegram-First, SMS-Fallback)

A localized notification system built for how Ethiopian parents actually stay reachable, without letting per-message costs scale linearly with every student and every event.

- **Telegram bot** as the default channel — free, unlimited, for any parent with a smartphone and Telegram installed
- **SMS reserved for two cases only:** parents without Telegram, and true emergency/school-closure broadcasts where guaranteed delivery matters more than cost
- Absence alerts, low-grade alerts, and fee due-date reminders sent via whichever channel the parent is registered on
- Term report card ready notification
- Optional companion web view for parents who want more detail than a notification provides

👉 This becomes the **trust engine** of EthioClass — parents feel informed without visiting the school, and the school doesn't pay a per-message fee for every parent who already has Telegram.

#### 5. 💵 Fee & Payment Tracking

Simple, per-term fee tracking in Ethiopian Birr.

- Per-term fee schedule per student
- Payment status: paid, partial, overdue
- Arrears carried forward automatically across terms
- Printable payment receipts
- "Who owes fees this term" report for the bursar

#### 6. 🌐 Multilingual Experience

Fully localized for Ethiopian schools:

- Amharic 🇪🇹
- English 🇬🇧

Includes:
- UI translation for teachers, admins, and the parent-facing view
- Report cards printed bilingually
- SMS templates in Amharic or English, school's choice

#### 7. 👤 Role-Based Access

Each user sees only what's relevant to their role:

- **School Admin** — full access, all classes, fee reports
- **Teacher** — their own classes and subjects only
- **Bursar** — fee records only, no grade access
- **Parent** — their own child's record only, read-only

---

### 💡 Additional Smart Features

#### 📊 Reporting Dashboards
- Class-level average and pass-rate trends across terms
- Attendance trend per student, flagged if dropping
- Fee-collection rate per term, school-wide

#### 🎒 School Setup Tools
- Bulk student import (from an existing Excel register, one-time migration)
- Subject and class configuration per grade level

---

### 🌟 What Makes EthioClass Unique

- 🇪🇹 Built specifically for Ethiopian private schools (not a global school-software adaptation)
- 💬 Full Amharic + English support, in reports and communication
- 📱 Telegram-first, SMS-fallback parent communication — free where possible, reliable where it matters, never dependent on email
- 📝 Grading and report cards match Ethiopia's actual academic conventions
- 📅 Ethiopian calendar integrated natively, not bolted on
- 🧾 Fee tracking built around Ethiopian termly billing, in Birr

---

### 🎯 MVP Scope (First Release)

To launch fast and validate with a handful of pilot schools:

- Student record & enrollment per class
- Grade entry and auto-calculated term averages/ranking
- One-click report card PDF generation (bilingual)
- Basic attendance logging
- Telegram bot alerts for absence and report-card-ready notices, with SMS fallback for parents without Telegram
- Fee tracking per term, paid/unpaid status
- Role-based login (admin, teacher, parent)

---

### 🚀 Future Expansion

- Parent mobile app with richer detail (not just SMS)
- Multi-branch support for school groups with several campuses
- Integration with the Ministry of Education reporting requirements
- Teacher lesson-plan and curriculum tracking
- Online fee payment (Telebirr/Chapa integration)